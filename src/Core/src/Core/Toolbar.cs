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

	public class Toolbar : Microsoft.Maui.IElement
	{
		public IElementHandler? Handler { get; set; }
		Maui.IElement? Maui.IElement.Parent => null;
	}
}
