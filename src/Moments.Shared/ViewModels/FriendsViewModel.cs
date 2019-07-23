using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;

namespace Moments
{
	public class FriendsViewModel : BaseViewModel
	{
		ObservableCollection<User> friends;
		Command fetchFriendsCommand;

		public FriendsViewModel ()
		{
			friends = FriendService.Instance.Friends;
		}

		public ObservableCollection<User> Friends
		{
			get { return friends; }
			set { friends = value; } 
		}

		public Command FetchFriendsCommand
		{
			get { return fetchFriendsCommand ?? (fetchFriendsCommand = new Command (async () => await ExecuteFetchFriendsCommand ()));}
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
					await FriendService.Instance.RefreshFriendsList ();
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