using CommunityToolkit.Maui;
using GetYourPlaceApp.Handlers;
using GetYourPlaceApp.Helpers;
using GetYourPlaceApp.Repository.Filter;
using GetYourPlaceApp.Repository.Login;
using GetYourPlaceApp.Repository.Properties;
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

        builder.Services.AddSingleton<MyProperties>();
        builder.Services.AddSingleton<MyPropertiesViewModel>();

        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<AppShellViewModel>();

        builder.Services.AddSingleton<BlankPage>();

        builder.Services.AddSingleton<LoginPage>();

        builder.Services.AddSingleton<LoginViewModel>();

        builder.Services.AddSingleton<PropertyDetail>();

        builder.Services.AddSingleton<PropertyDetailViewModel>();

        builder.Services.AddScoped<ILoginRepository, LoginRepository>();

        builder.Services.AddScoped<IFilterRepository, FilterRepository>();

        builder.Services.AddScoped<IPropertiesRepository, PropertiesRepository>();

        FormHandler.AdjustBorders();

        var app = builder.Build();
        ServiceHelper.Initialize(app.Services);

        return app;
	}
}
