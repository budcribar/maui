﻿#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Maui.Graphics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Color = Microsoft.Maui.Graphics.Color;

namespace Microsoft.Maui.Controls.Platform
{
	internal static class ToolbarExtensions
	{
		public static void UpdateIsVisible(this WindowHeader nativeToolbar, ControlsToolbar toolbar)
		{
			nativeToolbar.Visibility = (toolbar.IsVisible) ? UI.Xaml.Visibility.Visible : UI.Xaml.Visibility.Collapsed;
			if (nativeToolbar.NavigationView?.HeaderContent != null)
				nativeToolbar.NavigationView.HeaderContent.Visibility = nativeToolbar.Visibility;
		}

		public static void UpdateTitleIcon(this WindowHeader nativeToolbar, ControlsToolbar toolbar)
		{
			_ = toolbar?.Handler?.MauiContext ?? throw new ArgumentNullException(nameof(toolbar.Handler.MauiContext));
			toolbar.TitleIcon.LoadImage(toolbar.Handler.MauiContext, (result) =>
			{
				if (result != null)
				{
					nativeToolbar.TitleIconImageSource = result.Value;
					toolbar.Handler.UpdateValue(nameof(ControlsToolbar.IconColor));
				}
				else
					nativeToolbar.TitleIconImageSource = null;
			});
		}

		public static void UpdateBackButton(this WindowHeader nativeToolbar, ControlsToolbar toolbar)
		{
			if (nativeToolbar.NavigationView == null)
				return;

			nativeToolbar
				.NavigationView
				.IsBackButtonVisible = (toolbar.BackButtonVisible) ? NavigationViewBackButtonVisible.Visible : NavigationViewBackButtonVisible.Collapsed;

			nativeToolbar.NavigationView.IsBackEnabled = toolbar.BackButtonVisible;
			toolbar.Handler?.UpdateValue(nameof(ControlsToolbar.BarBackground));
		}

		public static void UpdateBarBackgroundColor(this WindowHeader nativeToolbar, ControlsToolbar toolbar)
		{
			UpdateBarBackground(nativeToolbar, toolbar);
		}

		public static void UpdateBarBackground(this WindowHeader nativeToolbar, ControlsToolbar toolbar)
		{
			if (nativeToolbar.NavigationView == null)
				return;


			var barBackground = toolbar.BarBackground;
			var barBackgroundColor = toolbar.BarBackgroundColor;

			nativeToolbar.NavigationView.UpdateBarBackgroundBrush(
				   barBackground?.ToBrush() ?? barBackgroundColor?.ToNative());
		}

		public static void UpdateTitleView(this WindowHeader nativeToolbar, ControlsToolbar toolbar)
		{
			_ = toolbar.Handler?.MauiContext ?? throw new ArgumentNullException(nameof(toolbar.Handler.MauiContext));

			nativeToolbar.TitleView = toolbar.TitleView?.ToNative(toolbar.Handler.MauiContext);
		}

		public static void UpdateIconColor(this WindowHeader nativeToolbar, ControlsToolbar toolbar)
		{
			// This property wasn't wired up in Controls
		}

		public static void UpdateTitle(this WindowHeader nativeToolbar, ControlsToolbar toolbar)
		{
			nativeToolbar.Title = toolbar.Title;
		}

		public static void UpdateBarTextColor(this WindowHeader nativeToolbar, ControlsToolbar toolbar)
		{
			if (toolbar.BarTextColor != null)
				nativeToolbar.TitleColor = toolbar.BarTextColor.ToNative();
		}

		public static void UpdateToolbarDynamicOverflowEnabled(this WindowHeader nativeToolbar, ControlsToolbar toolbar)
		{
			if (nativeToolbar.CommandBar == null)
				return;

			nativeToolbar.CommandBar.IsDynamicOverflowEnabled = toolbar.DynamicOverflowEnabled;
		}
	}
}
