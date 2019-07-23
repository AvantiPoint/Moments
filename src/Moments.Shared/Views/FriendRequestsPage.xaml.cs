using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Moments.Views
{
	public partial class FriendRequestsPage : BasePage
	{
		public FriendRequestsPage ()
		{
			BindingContext = new FriendRequestsPage ();

			InitializeComponent ();
			SetupToolbar ();
			SetupEventHandlers ();
		}

		private FriendRequestsViewModel ViewModel
		{
			get { return BindingContext as FriendRequestsViewModel; }
		}

		private void SetupToolbar ()
		{
			ToolbarItems.Add (new ToolbarItem {
				Icon = Images.CancelButton,
				Command = new Command (() => Navigation.PopModalAsync (true)),
				Priority = 1
			});
		}

		private void SetupEventHandlers ()
		{
			pendingFriendsListView.Refreshing += (sender, e) => {
				pendingFriendsListView.IsRefreshing = true;
				ViewModel.FetchFriendRequestsCommand.Execute (null);
				pendingFriendsListView.IsRefreshing = false;
			};
		}
	}
}