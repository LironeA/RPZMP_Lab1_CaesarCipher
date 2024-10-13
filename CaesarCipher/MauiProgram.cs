using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using CaesarCipher.ViewModel;
using CaesarCipher.Services;
using CaesarCipher.Views;

namespace CaesarCipher
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder .UseMauiApp<App>()
                    .UseMauiCommunityToolkit()
                    .ConfigureFonts(fonts =>
                    {
                        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                        fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            // Реєстрація сервісів
            builder.Services.AddSingleton<EncryptionService>();
            builder.Services.AddSingleton<FileService>();  // Реєстрація FileService
            builder.Services.AddTransient<CaesarPageVM>();
            builder.Services.AddTransient<CaesarPage>();
            builder.Services.AddTransient<VigenerePageVM>();
            builder.Services.AddTransient<VigenerePage>();
            return builder.Build();
        }
    }
}
