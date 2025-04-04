using Microsoft.Extensions.Logging;
using VibeShelf.App.Services;
using VibeShelf.App.ViewModels;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Maui;

namespace VibeShelf.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "vibeshelf.db");
            builder.Services.AddSingleton(new BookService(dbPath));

            builder.Services.AddSingleton<IBookSearchService, BookSearchService>();

            builder.Services.AddSingleton<FavoriteViewModel>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<SearchViewModel>();
            builder.Services.AddSingleton(new FavoriteBookService(dbPath));

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
