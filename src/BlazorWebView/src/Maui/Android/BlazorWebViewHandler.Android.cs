using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Android.Webkit;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using Microsoft.Maui.Handlers;
using static Android.Views.ViewGroup;
using AWebView = Android.Webkit.WebView;
using Path = System.IO.Path;

namespace Microsoft.AspNetCore.Components.WebView.Maui
{
	public partial class BlazorWebViewHandler : ViewHandler<IBlazorWebView, AWebView>
	{
		private WebViewClient? _webViewClient;
		private WebChromeClient? _webChromeClient;
		private AndroidWebKitWebViewManager? _webviewManager;
		internal AndroidWebKitWebViewManager? WebviewManager => _webviewManager;

		protected override AWebView CreateNativeView()
		{
			var aWebView = new AWebView(Context!)
			{
#pragma warning disable 618 // This can probably be replaced with LinearLayout(LayoutParams.MatchParent, LayoutParams.MatchParent); just need to test that theory
				LayoutParameters = new Android.Widget.AbsoluteLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent, 0, 0)
#pragma warning restore 618
			};

			AWebView.SetWebContentsDebuggingEnabled(enabled: true);

			if (aWebView.Settings != null)
			{
				aWebView.Settings.JavaScriptEnabled = true;
				aWebView.Settings.DomStorageEnabled = true;
			}

			_webViewClient = GetWebViewClient();
			aWebView.SetWebViewClient(_webViewClient);

			_webChromeClient = GetWebChromeClient();
			aWebView.SetWebChromeClient(_webChromeClient);

			return aWebView;
		}

		protected override void DisconnectHandler(AWebView nativeView)
		{
			nativeView.StopLoading();

			if (_webviewManager != null)
			{
				// Dispose this component's contents and block on completion so that user-written disposal logic and
				// Blazor disposal logic will complete.
				_webviewManager?
					.DisposeAsync()
					.AsTask()
					.GetAwaiter()
					.GetResult();

				_webviewManager = null;
			}

			_webViewClient?.Dispose();
			_webChromeClient?.Dispose();
		}

		private bool RequiredStartupPropertiesSet =>
			//_webview != null &&
			HostPage != null &&
			Services != null;

		private void StartWebViewCoreIfPossible()
		{
			if (!RequiredStartupPropertiesSet ||
				_webviewManager != null)
			{
				return;
			}
			if (NativeView == null)
			{
				throw new InvalidOperationException($"Can't start {nameof(BlazorWebView)} without native web view instance.");
			}

			// We assume the host page is always in the root of the content directory, because it's
			// unclear there's any other use case. We can add more options later if so.
			var contentRootDir = Path.GetDirectoryName(HostPage!) ?? string.Empty;
			var hostPageRelativePath = Path.GetRelativePath(contentRootDir, HostPage!);

			var customFileProvider = VirtualView.CreateFileProvider(contentRootDir);
			var mauiAssetFileProvider = new AndroidMauiAssetFileProvider(Context.Assets, contentRootDir);
			IFileProvider fileProvider = customFileProvider == null
				? mauiAssetFileProvider
				: new CompositeFileProvider(customFileProvider, mauiAssetFileProvider);

			_webviewManager = new AndroidWebKitWebViewManager(this, NativeView, Services!, ComponentsDispatcher, mauiAssetFileProvider, VirtualView.JSComponents, hostPageRelativePath);

			if (RootComponents != null)
			{
				foreach (var rootComponent in RootComponents)
				{
					// Since the page isn't loaded yet, this will always complete synchronously
					_ = rootComponent.AddToWebViewManagerAsync(_webviewManager);
				}
			}

			_webviewManager.Navigate("/");
		}

		protected virtual WebViewClient GetWebViewClient() =>
			new WebKitWebViewClient(this);

		protected virtual WebChromeClient GetWebChromeClient() =>
			new WebChromeClient();
	}
}
