using Microsoft.Extensions.Logging;

namespace Quiz
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
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("Poppins-Black.ttf", "Poppins-Black");
                    fonts.AddFont("Poppins-Bold.ttf", "Poppins-Bold");
                    fonts.AddFont("Poppins-Regular.ttf", "Poppins-regular");
                    fonts.AddFont("Poppins-Medium.ttf", "Poppins-Medium");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
