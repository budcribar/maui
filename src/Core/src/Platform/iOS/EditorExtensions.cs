using Microsoft.Maui.Graphics;
using ObjCRuntime;
using UIKit;

namespace Microsoft.Maui.Platform
{
	public static class EditorExtensions
	{
		public static void UpdatePlaceholder(this MauiTextView textView, IEditor editor)
			=> textView.PlaceholderText = editor.Placeholder;

		public static void UpdatePlaceholderColor(this MauiTextView textView, IEditor editor, UIColor? defaultPlaceholderColor)
			=> textView.PlaceholderTextColor = editor.PlaceholderColor?.ToNative() ?? defaultPlaceholderColor;

		public static void UpdateHorizontalTextAlignment(this MauiTextView textView, ITextAlignment textAlignment)
		{
			// We don't have a FlowDirection yet, so there's nothing to pass in here. 
			// TODO ezhart Update this when FlowDirection is available 
			// (or update the extension to take an IEditor instead of an alignment and work it out from there) 
			textView.TextAlignment = textAlignment.HorizontalTextAlignment.ToNative(true);
		}
	}
}
