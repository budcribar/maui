#nullable enable
using System;
using Android.App;
using Android.Views;
using AndroidX.AppCompat.App;
using Microsoft.Maui.Handlers;
using AView = Android.Views.View;

namespace Microsoft.Maui.Controls
{
	public partial class FlyoutPage
	{
		internal AView View =>
			(Handler?.NativeView as AView) ?? throw new InvalidOperationException("NativeView not set.");

		void UpdateToolbar()
		{
			var appbarLayout = View.FindViewById<ViewGroup>(Microsoft.Maui.Resource.Id.navigationlayout_appbar);

			if (appbarLayout == null || this is not IToolbarElement te || Handler?.MauiContext == null)
				return;

			var nativeToolBar = te.Toolbar?.ToNative(Handler.MauiContext, true);
			if (nativeToolBar == null || nativeToolBar?.Parent == nativeToolBar)
				return;

			appbarLayout.AddView(nativeToolBar, 0);
			
		}

		public static void MapToolbar(FlyoutViewHandler handler, FlyoutPage view)
		{
			view.UpdateToolbar();
		}
	}
}