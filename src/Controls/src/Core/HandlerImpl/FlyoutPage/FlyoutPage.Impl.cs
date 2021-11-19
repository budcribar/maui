#nullable enable

namespace Microsoft.Maui.Controls
{
	public partial class FlyoutPage : IFlyoutView, IToolbarElement
	{
		IView IFlyoutView.Flyout => this.Flyout;
		IView IFlyoutView.Detail => this.Detail;

		Toolbar IToolbarElement.Toolbar => _toolBar ??= new ControlsToolbar(this);
		Toolbar? _toolBar;
	}
}
