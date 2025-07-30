using LibraryAppIMFinal.Models;
using System.Collections.ObjectModel;

namespace LibraryAppIMFinal;

public partial class SearchResults : ContentPage
{
    public ObservableCollection<Media> SearchResultsList { get; set; } = new();
    private APIService _apiService = new(); // create instance of your API wrapper

    public SearchResults(SearchResult searchParams)
    {
        InitializeComponent();
        BindingContext = this;
        LoadSearchResults(searchParams);
    }

    private async void LoadSearchResults(SearchResult searchParams)
    {
        try
        {
            var allMedia = await _apiService.GetMediaAsync();

            var filtered = allMedia.Where(m =>
                (string.IsNullOrWhiteSpace(searchParams.Title) || m.Title.Contains(searchParams.Title, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrWhiteSpace(searchParams.Author) || m.Author.Contains(searchParams.Author, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrWhiteSpace(searchParams.Genre) || m.Genre.Contains(searchParams.Genre, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrWhiteSpace(searchParams.ISBN) || m.ISBN.Contains(searchParams.ISBN, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrWhiteSpace(searchParams.Series) || m.Series.Contains(searchParams.Series, StringComparison.OrdinalIgnoreCase))
            ).ToList();

            SearchResultsList.Clear();
            foreach (var item in filtered)
            {
                SearchResultsList.Add(item);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not load results: {ex.Message}", "OK");
        }
    }
}
