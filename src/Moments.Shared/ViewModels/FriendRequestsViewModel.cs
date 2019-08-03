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
using System.Reactive;
using ReactiveUI;

namespace Moments.ViewModels
{
    public class FriendRequestsViewModel : BaseViewModel
    {
        private IFriendService FriendService { get; }

        public FriendRequestsViewModel(INavigationService navigationService, IDialogService dialogService, IFriendService friendService, ILogger logger)
            : base(navigationService, dialogService, logger)
        {
            FriendService = friendService;
            Title = Strings.FriendRequests;
            FriendRequests = FriendService.PendingFriends;

            FetchFriendRequestsCommand = ReactiveCommand.CreateFromTask(ExecuteFetchFriendsCommand);
            ConfirmCommand = ReactiveCommand.CreateFromTask<User>(OnConfirmCommandExecuted);
            DenyCommand = ReactiveCommand.CreateFromTask<User>(OnDenyCommandExecuted);
        }

        public ObservableCollection<User> FriendRequests { get; }

        public ReactiveCommand<Unit, Unit> FetchFriendRequestsCommand { get; }

        public ReactiveCommand<User, Unit> ConfirmCommand { get; }

        public ReactiveCommand<User, Unit> DenyCommand { get; }

        private async Task ExecuteFetchFriendsCommand()
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

        private async Task OnConfirmCommandExecuted(User user)
        {
            await FriendService.AcceptFriendship(user);
        }

        private async Task OnDenyCommandExecuted(User user)
        {
            await FriendService.DenyFriendship(user);
        }
    }
}

