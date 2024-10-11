using Avatab.Services;
using Avatab.Services.Interfaces;
using Avatab.ViewModel;
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
