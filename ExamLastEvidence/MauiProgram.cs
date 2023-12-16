using ExamLastEvidence.Services;
using ExamLastEvidence.ViewModels;
using ExamLastEvidence.Views;
using Microsoft.Extensions.Logging;

namespace ExamLastEvidence
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
            builder.Services.AddSingleton<ProductService>();
            builder.Services.AddSingleton<ProductCollection>();
            builder.Services.AddSingleton<ProductListContenPage>();
            builder.Services.AddSingleton<ProductDetails>();
            return builder.Build();
        }
    }
}
