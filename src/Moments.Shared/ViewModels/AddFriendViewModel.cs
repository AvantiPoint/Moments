using System;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Moments.Mvvm;
using Moments.Services;
using Xamarin.Forms;

namespace Moments
{
    public class AddFriendViewModel : BaseViewModel
    {
        private IFriendService FriendService { get; }

        public AddFriendViewModel(IFriendService friendService)
        {
            FriendService = friendService;
        }

        string username;
        Command addFriendCommand;

        public string Username
        {
            get { return username; }
            set { username = value; OnPropertyChanged("Username"); }
        }

        public Command AddFriendCommand
        {
            get { return addFriendCommand ?? (addFriendCommand = new Command(async () => await ExecuteAddFriendCommand())); }
        }

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