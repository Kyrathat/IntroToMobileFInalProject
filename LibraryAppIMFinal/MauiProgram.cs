using Microsoft.Extensions.Logging;

namespace LibraryAppIMFinal
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Register services and pages
            builder.Services.AddSingleton<APIService>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<SearchResults>();
            builder.Services.AddTransient<BookCreation>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}
