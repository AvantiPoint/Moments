using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Moments.Helpers;
using Moments.Mvvm;
using Moments.Services;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services.Dialogs;
using Xamarin.Forms;

namespace Moments
{
    public class FriendsViewModel : BaseViewModel
    {
        private IFriendService FriendService { get; }
        ObservableCollection<User> friends;
        Command fetchFriendsCommand;

        public FriendsViewModel(INavigationService navigationService, IDialogService dialogService, ILogger logger, IFriendService friendService)
            : base(navigationService, dialogService, logger)
        {
            FriendService = friendService;
            friends = FriendService.Friends;
        }

        public ObservableCollection<User> Friends
        {
            get { return friends; }
            set { friends = value; }
        }

        public Command FetchFriendsCommand
        {
            get { return fetchFriendsCommand ?? (fetchFriendsCommand = new Command(async () => await ExecuteFetchFriendsCommand())); }
        }

        public async Task ExecuteFetchFriendsCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                if (await ConnectivityService.IsConnected())
                {
                    await FriendService.RefreshFriendsList();
                }
                else
                {
                    DialogService.ShowError(Strings.NoInternetConnection);
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