#nullable enable
using System;
using Android.App;
using Android.Views;
using AndroidX.AppCompat.App;
using Microsoft.Maui.Handlers;

namespace Microsoft.Maui.Controls
{
	public partial class Window
	{
		internal Activity NativeActivity =>
			(Handler?.NativeView as Activity) ?? throw new InvalidOperationException("Window should have an Activity set.");


		void UpdateToolbar()
		{
			var appbarLayout = NativeActivity.FindViewById<ViewGroup>(Microsoft.Maui.Resource.Id.navigationlayout_appbar);

			if (appbarLayout == null || this is not IToolbarElement te || Handler?.MauiContext == null)
				return;

			var nativeToolBar = te.Toolbar?.ToNative(Handler.MauiContext, true);
			if (nativeToolBar == null || nativeToolBar.Parent == nativeToolBar)
				return;

			appbarLayout.AddView(nativeToolBar, 0);

			// Visibility can only be updated after layout has been set for the toolbar
			te.Toolbar?.Handler?.UpdateValue(nameof(ControlsToolbar.IsVisible));
			
		}

		public static void MapToolbar(WindowHandler handler, IWindow view)
		{
			if (view is Window w)
				w.UpdateToolbar();
		}

		public static void MapContent(WindowHandler handler, IWindow view)
		{
			if (view.Content is not Shell)
			{
				WindowHandler.MapContent(handler, view);
				return;
			}

			var nativeContent = view.Content.ToContainerView(handler.MauiContext!);
			handler.NativeView.SetContentView(nativeContent);

			if (view is Window w)
				w.UpdateToolbar();
		}
	}
}