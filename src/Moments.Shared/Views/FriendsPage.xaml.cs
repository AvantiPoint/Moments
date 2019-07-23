using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Moments.Views
{
	public partial class FriendsPage
	{
		public FriendsPage ()
		{
			BindingContext = new FriendsViewModel ();

			InitializeComponent ();

			SetupToolbar ();
			SetupEventHandlers ();
		}

		private FriendsViewModel ViewModel
		{
			get { return BindingContext as FriendsViewModel; }
		}

		private void SetupToolbar ()
		{
			ToolbarItems.Add (new ToolbarItem {
				IconImageSource = Images.AddFriendButton,
				Command = new Command (() => Navigation.PushModalAsync (new AddFriendPage (), true))
			});

			if (Device.RuntimePlatform != Device.iOS) {
				ToolbarItems.Add (new ToolbarItem {
					IconImageSource = Images.FriendRequestsButton,
					Command = new Command (() => Navigation.PushModalAsync (new NavigationPage (new FriendRequestsPage ()) {
						BarBackgroundColor = Colors.NavigationBarColor,
						BarTextColor = Colors.NavigationBarTextColor
					}, true)),
					Priority = 1
				});
			}
		}

		private void SetupEventHandlers ()
		{
			friendsListView.ItemSelected += (s, e) => {
				friendsListView.SelectedItem = null;
			};

			friendsListView.Refreshing += (sender, e) => {
				friendsListView.IsRefreshing = true;
				ViewModel.FetchFriendsCommand.Execute (null);
				friendsListView.IsRefreshing = false;
			};
		}
	}
}

