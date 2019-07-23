using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;

namespace Moments
{
	public class FriendRequestsViewModel : BaseViewModel
	{
		ObservableCollection<User> friendRequests;

		Command fetchFriendRequestsCommand;

		public FriendRequestsViewModel ()
		{
			Title = Strings.FriendRequests;
			friendRequests = FriendService.Instance.PendingFriends;
		}

		public ObservableCollection<User> FriendRequests
		{
			get { return friendRequests; }
			set { friendRequests = value; } 
		}

		public Command FetchFriendRequestsCommand
		{
			get { return fetchFriendRequestsCommand ?? (fetchFriendRequestsCommand = new Command (async () => await ExecuteFetchFriendsCommand ())); }
		}

		public async Task ExecuteFetchFriendsCommand ()
		{
			if (IsBusy) {
				return;
			}

			IsBusy = true;

			try
			{
				if (await ConnectivityService.IsConnected ()) {
					await FriendService.Instance.RefreshPendingFriendsList ();
				} else {
					DialogService.ShowError (Strings.NoInternetConnection);
				}
			}
			catch (Exception ex) 
			{
                Crashes.TrackError(ex);
			}

			IsBusy = false;
		}
	}
}

