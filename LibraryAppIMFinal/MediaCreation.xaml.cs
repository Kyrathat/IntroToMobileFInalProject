using LibraryAppIMFinal.Models;

namespace LibraryAppIMFinal
{
    public partial class BookCreation : ContentPage
    {
        private readonly APIService _apiService;

        public BookCreation(APIService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        private async void OnAddBookClick(object sender, EventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(TitleEntry.Text))
            {
                await DisplayAlert("Validation Error", "Title is required.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(AuthorEntry.Text))
            {
                await DisplayAlert("Validation Error", "Author is required.", "OK");
                return;
            }

            try
            {
                // Create new Media object
                var newBook = new Media
                {
                    Title = TitleEntry.Text.Trim(),
                    Author = AuthorEntry.Text.Trim(),
                    Genre = GenreEntry.Text?.Trim() ?? string.Empty,
                    ISBN = ISBNEntry.Text?.Trim() ?? string.Empty,
                    Series = SeriesEntry.Text?.Trim() ?? string.Empty,
                    // Add other properties as needed based on your Media model
                };

                // Add publication year if provided and valid
                if (!string.IsNullOrWhiteSpace(PublicationYearEntry.Text) &&
                    int.TryParse(PublicationYearEntry.Text, out int year))
                {
                    // Assuming your Media model has a PublicationYear property
                    // newBook.PublicationYear = year;
                }

                // Add description if provided
                if (!string.IsNullOrWhiteSpace(DescriptionEditor.Text))
                {
                    // Assuming your Media model has a Description property
                    // newBook.Description = DescriptionEditor.Text.Trim();
                }

                // Call API to create the book
                var createdBook = await _apiService.PostMediaAsync(newBook);

                // Show success message
                await DisplayAlert("Success",
                    $"'{newBook.Title}' has been added to the library successfully!",
                    "OK");

                // Clear the form
                ClearForm();

                // Optionally navigate back
                // await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error",
                    $"Failed to add book to library: {ex.Message}",
                    "OK");
            }
        }

        private void OnClearClick(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            TitleEntry.Text = string.Empty;
            AuthorEntry.Text = string.Empty;
            GenreEntry.Text = string.Empty;
            ISBNEntry.Text = string.Empty;
            SeriesEntry.Text = string.Empty;
            PublicationYearEntry.Text = string.Empty;
            DescriptionEditor.Text = string.Empty;
        }

        protected override bool OnBackButtonPressed()
        {
            // Show confirmation dialog when user tries to go back
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("Confirm",
                    "Are you sure you want to go back? Any unsaved changes will be lost.",
                    "Yes", "No");

                if (result)
                {
                    await Navigation.PopAsync();
                }
            });

            return true; // Prevent default back navigation
        }
    }
}