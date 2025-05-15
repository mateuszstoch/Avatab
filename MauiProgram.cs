using Avatab.Services;
using Avatab.Services.Interfaces;
using Avatab.ViewModel;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;



namespace Avatab
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

#if DEBUG
                builder.Logging.AddDebug();
#endif
            //pages
            builder.Services.AddSingleton<MainPage>();

            //view models
            builder.Services.AddSingleton<MainViewModel>();


            //services
            builder.Services.AddSingleton<IDatabaseService,DatabaseService>();

            return builder.Build();
        }
    }
}
