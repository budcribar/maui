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
							Detail = new NavigationPage(CreatePage()) { Title = "Page 1" }
						},
						new FlyoutPage()
						{
							Title = "tab 2",
							Flyout = CreatePage(),
							Detail = new NavigationPage(CreatePage()) { Title = "Page 2" }
						},
						new NavigationPage(CreatePage())
						{
							Title = "Page 3"
						}
					}
				});

		//new Window(
		//	new FlyoutPage()
		//	{
		//		Flyout = CreatePage(),
		//		Detail = new NavigationPage(CreatePage()) { Title = "Detail" }
		//	});

		int i = 0;
		ContentPage CreatePage()
		{
			ContentPage page = null;

			page = new ContentPage
			{
				Title = $"Title {++i}",
				BackgroundColor = Colors.Purple,
				Content = new Button
				{
					BackgroundColor = Colors.Purple,
					Text = "Hello Sandbox!",
					HorizontalOptions = LayoutOptions.Center,
					VerticalOptions = LayoutOptions.Center,
					Command = new Command(async () =>
					{
						await page.Navigation.PushAsync(CreatePage());
					})
				}
			};

			return page;
		}
	}
}