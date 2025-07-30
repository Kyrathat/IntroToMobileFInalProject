using LibraryAppIMFinal.Models;

namespace LibraryAppIMFinal
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async  void OnSearchClick(object sender, EventArgs e)
        {
            var searchParams = new SearchResult
            {
                Title = TitleEntry.Text,
                Author = AuthorEntry.Text,
                Genre = GenreEntry.Text,
                ISBN = ISBNEntry.Text,
                Series = SeriesEntry.Text,
            };

            var searchPage = new SearchResults(searchParams);
            await Navigation.PushAsync(searchPage);
        }
    }

}
