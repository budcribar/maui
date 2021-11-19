#nullable enable

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Handlers;

namespace Microsoft.Maui.Controls.Handlers
{
#if ANDROID || WINDOWS
	public partial class ToolbarHandler
	{
		public static IPropertyMapper<ControlsToolbar, ToolbarHandler> Mapper =
			   new PropertyMapper<ControlsToolbar, ToolbarHandler>(ElementMapper)
			   {
				   [nameof(ControlsToolbar.IsVisible)] = MapIsVisible,
				   [nameof(ControlsToolbar.BackButtonVisible)] = MapBackButtonVisible,
				   [nameof(ControlsToolbar.TitleIcon)] = MapTitleIcon,
				   [nameof(ControlsToolbar.TitleView)] = MapTitleView,
				   [nameof(ControlsToolbar.IconColor)] = MapIconColor,
				   [nameof(ControlsToolbar.Title)] = MapTitle,
				   [nameof(ControlsToolbar.ToolbarItems)] = MapToolbarItems,
				   [nameof(ControlsToolbar.BackButtonTitle)] = MapBackButtonTitle,
				   [nameof(ControlsToolbar.BarBackgroundColor)] = MapBarBackgroundColor,
				   [nameof(ControlsToolbar.BarBackground)] = MapBarBackground,
				   [nameof(ControlsToolbar.BarTextColor)] = MapBarTextColor,
				   [nameof(ControlsToolbar.IconColor)] = MapIconColor,
#if WINDOWS
				   [PlatformConfiguration.WindowsSpecific.Page.ToolbarPlacementProperty.PropertyName] = MapToolbarPlacement,
				   [PlatformConfiguration.WindowsSpecific.Page.ToolbarDynamicOverflowEnabledProperty.PropertyName] = MapToolbarDynamicOverflowEnabled,
#endif
			   };

		public static CommandMapper<ControlsToolbar, ToolbarHandler> CommandMapper = new()
		{
		};

		public ToolbarHandler() : base(Mapper, CommandMapper)
		{
		}
	}
#endif
}
