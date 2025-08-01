using LibraryAppIMFinal.Models;
using System.Collections.ObjectModel;

namespace LibraryAppIMFinal;

public partial class SearchResults : ContentPage
{
    public ObservableCollection<Media> SearchResultsList { get; set; } = new();
    public bool IsLoading { get; set; } = true;
    public bool HasResults => SearchResultsList.Count > 0;
    public bool HasNoResults => !IsLoading && SearchResultsList.Count == 0;
    public string ResultsText => SearchResultsList.Count == 1 ? "1 result found" : $"{SearchResultsList.Count} results found";

    private readonly APIService _apiService;

    public SearchResults(APIService apiService, SearchResult searchParams)
    {
        InitializeComponent();
        _apiService = apiService;
        BindingContext = this;
        LoadSearchResults(searchParams);
    }

    private async void LoadSearchResults(SearchResult searchParams)
    {
        try
        {
            IsLoading = true;
            OnPropertyChanged(nameof(IsLoading));
            OnPropertyChanged(nameof(HasNoResults));

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

            IsLoading = false;
            OnPropertyChanged(nameof(IsLoading));
            OnPropertyChanged(nameof(HasResults));
            OnPropertyChanged(nameof(HasNoResults));
            OnPropertyChanged(nameof(ResultsText));
        }
        catch (Exception ex)
        {
            IsLoading = false;
            OnPropertyChanged(nameof(IsLoading));
            OnPropertyChanged(nameof(HasNoResults));

            await DisplayAlert("Search Error",
                "Unable to search the library database. Please check your connection and try again.",
                "OK");
        }
    }

    private async void OnBackToSearchClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnViewDetailsClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Coming Soon", "Feature coming soon.", "OK");
    }
}