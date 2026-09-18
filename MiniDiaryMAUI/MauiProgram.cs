using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Maui;
using Microsoft.Extensions.Logging;
using MiniDiaryMAUI.Services;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace MiniDiaryMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp()
                .UseLiveCharts()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            LiveCharts.Configure(config => config
                .AddSkiaSharp()
                .AddDefaultMappers()
                .AddLightTheme());

            builder.Services.AddSingleton<IDatabaseService, DatabaseService>();
            builder.Services.AddSingleton<IEntriesService, EntriesService>();
            builder.Services.AddSingleton<IDateTimeFormatterService, DateTimeFormatterService>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
