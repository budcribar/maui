using System;
using Microsoft.Maui.Handlers;

namespace Microsoft.Maui.Controls
{
	public partial class FlyoutPage
	{
#if ANDROID
		public static IPropertyMapper<FlyoutPage, FlyoutViewHandler> ControlsFlyoutPageMapper = new PropertyMapper<FlyoutPage, FlyoutViewHandler>(FlyoutViewHandler.Mapper)
		{
			[nameof(Toolbar)] = MapToolbar,
		};
#endif

		public new static void RemapForControls()
		{
#if ANDROID
			FlyoutViewHandler.Mapper = ControlsFlyoutPageMapper;
#endif
		}
	}
}
