using System;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.DeviceTests.Stubs;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Hosting;

namespace Microsoft.Maui.DeviceTests
{
	public partial class HandlerTestBase : TestBase, IDisposable
	{
		MauiApp _mauiApp;

		public HandlerTestBase()
		{
			_mauiApp = MauiApp
				.CreateBuilder()
				.ConfigureMauiHandlers(handlers =>
				{
					handlers.AddHandler(typeof(Editor), typeof(EditorHandler));
					handlers.AddHandler(typeof(VerticalStackLayout), typeof(LayoutHandler));
				})
				.Build();

			MauiContext = new ContextStub(_mauiApp.Services);
		}

		public void Dispose()
		{
			((IDisposable)_mauiApp).Dispose();

			_mauiApp = null;
			App = null;
			MauiContext = null;
		}

		protected IApplication App { get; private set; }

		protected IMauiContext MauiContext { get; private set; }

		protected THandler CreateHandler<THandler>(IView view)
			where THandler : IViewHandler
		{
			var handler = Activator.CreateInstance<THandler>();
			handler.SetMauiContext(MauiContext);

			handler.SetVirtualView(view);
			view.Handler = handler;

			view.Arrange(new Rectangle(0, 0, view.Width, view.Height));
			handler.NativeArrange(view.Frame);

			return handler;
		}

		protected async Task<THandler> CreateHandlerAsync<THandler>(IView view) where THandler : IViewHandler =>
			await InvokeOnMainThreadAsync(() => CreateHandler<THandler>(view));
	}
}