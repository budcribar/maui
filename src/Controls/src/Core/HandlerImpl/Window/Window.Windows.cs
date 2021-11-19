using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Maui.Handlers;

namespace Microsoft.Maui.Controls
{
	public partial class Window
	{
		internal UI.Xaml.Window NativeWindow =>
			(Handler?.NativeView as UI.Xaml.Window) ?? throw new InvalidOperationException("Window Handler should have a Window set.");

		public static void MapToolbar(WindowHandler handler, IWindow view)
		{
			_ = handler.MauiContext ?? throw new InvalidOperationException($"{nameof(handler.MauiContext)} null");

			if (view is IToolbarElement tb && tb.Toolbar != null)
			{
				_ = tb.Toolbar.ToNative(handler.MauiContext);
			}
		}

		public static void MapContent(WindowHandler handler, IWindow view)
		{
			if (view.Content is not Shell)
			{
				WindowHandler.MapContent(handler, view);
				return;
			}
			if (handler.NativeView.Content is UI.Xaml.Controls.Panel panel)
			{
				var nativeContent = view.Content.ToNative(handler.MauiContext!);
				panel.Children.Clear();
				panel.Children.Add(nativeContent);

			}

		}
	}
}
