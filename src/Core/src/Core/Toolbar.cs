using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Maui.Graphics;

namespace Microsoft.Maui
{
	internal interface IToolbarElement
	{
		Toolbar? Toolbar { get;}
	}

	public class Toolbar : IElement
	{
		bool _isVisible = false;
		bool _backButtonVisible;
		public IElementHandler? Handler { get; set; }
		IElement? IElement.Parent => null;

		public bool BackButtonVisible { get => _backButtonVisible; set => SetProperty(ref _backButtonVisible, value); }
		public bool IsVisible { get => _isVisible; set => SetProperty(ref _isVisible, value); }

		void SetProperty<T>(ref T backingStore, T value,
			[CallerMemberName] string propertyName = "")
		{
			if (EqualityComparer<T>.Default.Equals(backingStore, value))
				return;

			backingStore = value;
			Handler?.UpdateValue(propertyName);
		}
	}
}
