using CommunityToolkit.Maui;
using GetYourPlaceApp.Helpers;
using GetYourPlaceApp.Repository.Filter;
using GetYourPlaceApp.Repository.Login;
namespace GetYourPlaceApp;

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
			}).UseMauiCommunityToolkit();

		builder.Services.AddSingleton<MainViewModel>();

		builder.Services.AddSingleton<MainPage>();

		builder.Services.AddSingleton<BlankViewModel>();

        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<AppShellViewModel>();

        builder.Services.AddSingleton<BlankPage>();

        builder.Services.AddSingleton<LoginPage>();

        builder.Services.AddSingleton<LoginViewModel>();

        builder.Services.AddScoped<ILoginRepository, LoginRepository>();

        builder.Services.AddScoped<IFilterRepository, FilterRepository>();

        var app = builder.Build();
        ServiceHelper.Initialize(app.Services);

        return app;
	}
}
