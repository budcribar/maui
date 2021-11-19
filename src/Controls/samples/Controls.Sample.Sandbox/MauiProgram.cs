using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Hosting;

namespace Maui.Controls.Sample
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp() =>
			MauiApp
				.CreateBuilder()
				.UseMauiApp<App>()
				.Build();
	}

	class App : Application
	{
		protected override Window CreateWindow(IActivationState activationState) =>
			new Window(
				new TabbedPage()
				{
					Children =
					{
						new FlyoutPage()
						{
							Title = "Tab 1",
							Flyout = CreatePage(),
							Detail = new NavigationPage(CreatePage()) { Title = "Detail" }
						},
						new FlyoutPage()
						{
							Title = "tab 2",
							Flyout = CreatePage(),
							Detail = new NavigationPage(CreatePage()) { Title = "Detail" }
						},
						new NavigationPage(CreatePage())
						{
							Title = "navpage 2"
						}
					}
				});

		//new Window(
		//	new FlyoutPage()
		//	{
		//		Flyout = CreatePage(),
		//		Detail = new NavigationPage(CreatePage()) { Title = "Detail" }
		//	});


		ContentPage CreatePage() => new ContentPage
		{
			Title = "Title",
			BackgroundColor = Colors.Purple,
			Content = new Button
			{
				BackgroundColor = Colors.Purple,
				Text = "Hello Sandbox!",
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Command = new Command(async () =>
				{
					await 
					(Application.Current.MainPage as FlyoutPage).Detail.Navigation.PushAsync(CreatePage());
				})
			}
		};
	}
}