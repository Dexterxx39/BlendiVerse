using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using BlendiVerseApp.Services;
using BlendiVerseApp.ViewModels;
using BlendiVerseApp.Pages;

namespace BlendiVerseApp;
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
            fonts.AddFont("Lexend.ttf", "Lexend");
            fonts.AddFont("Lexend-Bold.ttf", "LexendBold");
        }).UseMauiCommunityToolkit();
        
#if DEBUG
        builder.Logging.AddDebug();
#endif

        AddSmoothieServices(builder.Services);
        return builder.Build();
    }

    private static IServiceCollection AddSmoothieServices(IServiceCollection services)
    {
        // Register FirestoreService before SmoothieService
        services.AddSingleton<FirestoreService>();  // Register FirestoreService
        services.AddSingleton<SmoothieService>();  // Register SmoothieService

        // Register pages and view models
        services.AddSingleton<HomePage>()
                .AddSingleton<HomeViewModel>();

        services.AddTransientWithShellRoute<AllSmoothiesPage, AllSmoothieViewModel>(nameof(AllSmoothiesPage));

        services.AddTransientWithShellRoute<DetailPage, DetailsViewModel>(nameof(DetailPage));
        
        services.AddSingleton<CartViewModel>();
        services.AddTransient<CartPage>();

        return services;
    }
}
