using LibraryAppIMFinal.Models;

namespace LibraryAppIMFinal
{
    public partial class MainPage : ContentPage
    {
        private readonly APIService _apiService;

        public MainPage(APIService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        private async void OnSearchClick(object sender, EventArgs e)
        {
            var searchParams = new SearchResult
            {
                Title = TitleEntry.Text,
                Author = AuthorEntry.Text,
                Genre = GenreEntry.Text,
                ISBN = ISBNEntry.Text,
                Series = SeriesEntry.Text,
            };
            var searchPage = new SearchResults(_apiService, searchParams);
            await Navigation.PushAsync(searchPage);
        }

        private void OnClearClick(object sender, EventArgs e)
        {
            TitleEntry.Text = string.Empty;
            AuthorEntry.Text = string.Empty;
            GenreEntry.Text = string.Empty;
            ISBNEntry.Text = string.Empty;
            SeriesEntry.Text = string.Empty;
        }

        private async void OnCreateBookClick(object sender, EventArgs e)
        {
            var mediaCreationPage = new BookCreation(_apiService);
            await Navigation.PushAsync(mediaCreationPage);
        }
    }
}