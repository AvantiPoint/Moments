using System;
using System.Reactive;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Moments.Helpers;
using Moments.Mvvm;
using Moments.Services;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services.Dialogs;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace Moments
{
    public class AddFriendViewModel : BaseViewModel
    {
        private IFriendService FriendService { get; }

        public AddFriendViewModel(INavigationService navigationService, IDialogService dialogService, ILogger logger, IFriendService friendService) : base(navigationService, dialogService, logger)
        {
            FriendService = friendService;
            AddFriendCommand = ReactiveCommand.CreateFromTask(ExecuteAddFriendCommand);
        }

        string username;
        Command addFriendCommand;

        [Reactive]public string Username { get; set; }

        public ReactiveCommand<Unit, Unit> AddFriendCommand { get; }

        private async Task ExecuteAddFriendCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                DialogService.ShowLoading(Strings.AddingFriend);
                if (await ConnectivityService.IsConnected())
                {
                    var success = await CreateFriendship();
                    DialogService.HideLoading();
                    if (success)
                    {
                        DialogService.ShowSuccess(Strings.FriendAdded);
                    }
                    else
                    {
                        DialogService.ShowError(Strings.FriendRequestFailed);
                    }
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

        private async Task<bool> CreateFriendship()
        {
            return await FriendService.CreateFriendship(Username);
        }
    }
}