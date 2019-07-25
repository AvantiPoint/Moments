using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Logging;
using Moments.Services;
using Xamarin.Forms;
using Moments.Mvvm;
using Prism.Services.Dialogs;
using Prism.Navigation;
using Moments.Helpers;

namespace Moments.ViewModels
{
    public class FriendRequestsViewModel : BaseViewModel
    {
        private IFriendService FriendService { get; }
        ObservableCollection<User> friendRequests;

        Command fetchFriendRequestsCommand;

        public FriendRequestsViewModel(INavigationService navigationService, IDialogService dialogService, IFriendService friendService, ILogger logger)
            : base(navigationService, dialogService, logger)
        {
            FriendService = friendService;
            Title = Strings.FriendRequests;
            friendRequests = FriendService.PendingFriends;
        }

        public ObservableCollection<User> FriendRequests
        {
            get { return friendRequests; }
            set { friendRequests = value; }
        }

        public Command FetchFriendRequestsCommand
        {
            get { return fetchFriendRequestsCommand ?? (fetchFriendRequestsCommand = new Command(async () => await ExecuteFetchFriendsCommand())); }
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
                    await FriendService.RefreshPendingFriendsList();
                }
                else
                {
                    DialogService.ShowError(Strings.NoInternetConnection);
                }
            }
            catch (Exception ex)
            {
                Logger.Report(ex);
            }

            IsBusy = false;
        }
    }
}

