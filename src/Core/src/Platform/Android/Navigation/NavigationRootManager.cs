using System;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.CoordinatorLayout.Widget;
using AndroidX.DrawerLayout.Widget;
using AndroidX.Fragment.App;
using Google.Android.Material.AppBar;
using AView = Android.Views.View;

namespace Microsoft.Maui
{
	public class NavigationRootManager
	{
		//Toolbar? _toolbar;
		View? _navigationLayout;
		IMauiContext _mauiContext;
		AView? _rootView;
		ViewFragment? _viewFragment;

		internal View NavigationLayout => _navigationLayout 
			?? throw new InvalidOperationException($"Resource.Layout.navigationlayout missing");

		LayoutInflater LayoutInflater => _mauiContext?.GetLayoutInflater()
			?? throw new InvalidOperationException($"LayoutInflater missing");

		internal FragmentManager FragmentManager => _mauiContext?.GetFragmentManager()
			?? throw new InvalidOperationException($"FragmentManager missing");

		public AView? RootView => _rootView;

		internal DrawerLayout? DrawerLayout { get; private set; }

		public NavigationRootManager(IMauiContext mauiContext)
		{
			_mauiContext = mauiContext;
		}

		internal void SetRootView(IView view, IMauiContext? mauiContext = null)
		{
			mauiContext = mauiContext ?? _mauiContext;
			var containerView = view.ToContainerView(mauiContext);
			_navigationLayout = containerView.FindViewById(Resource.Id.navigation_layout);
			_navigationLayout ??=
				LayoutInflater
					.Inflate(Resource.Layout.navigationlayout, null)
					.JavaCast<CoordinatorLayout>();

			if (containerView is DrawerLayout dl)
			{
				_rootView = dl;
				DrawerLayout = dl;
			}
			else if (containerView is ContainerView cv && cv.MainView is DrawerLayout dlc)
			{
				_rootView = cv;
				DrawerLayout = dlc;
			}
			else
			{
				_rootView = _navigationLayout;
			}
			
			if (DrawerLayout == null)
			{
				SetContentView(containerView);
			}
			else if(NavigationLayout.Parent == null)
			{
				NavigationLayout.LayoutParameters =
					new DrawerLayout.LayoutParams(DrawerLayout.LayoutParams.MatchParent, DrawerLayout.LayoutParams.MatchParent);

				DrawerLayout.AddView(NavigationLayout, 0);
			}
		}

		internal virtual void SetContentView(AView? view) =>
			SetContentView(view, FragmentManager);

		internal virtual void SetContentView(AView? view, FragmentManager fragmentManager)
		{
			if (view == null)
			{
				if(_viewFragment != null)
				{
					FragmentManager
						.BeginTransaction()
						.Remove(_viewFragment)
						.SetReorderingAllowed(true)
						.Commit();
				}
			}
			else
			{
				_viewFragment = new ViewFragment(view);
				fragmentManager
						.BeginTransaction()
						.Replace(Resource.Id.navigationlayout_content, _viewFragment)
						.SetReorderingAllowed(true)
						.Commit();
			}
		}
	}
}
