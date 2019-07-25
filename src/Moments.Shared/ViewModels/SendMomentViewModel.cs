using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Moments.Mvvm;
using Moments.Services;
using Prism.Logging;
using Xamarin.Forms;
using Prism.Navigation;
using Prism.Services.Dialogs;
using ReactiveUI.Fody.Helpers;
using Moments.Helpers;

namespace Moments.ViewModels
{
    public class SendMomentViewModel : BaseViewModel
    {
        private IFriendService FriendService { get; }
        private IMomentService MomentService { get; }

        byte[] imageData;
        int displayTime;

        Command sendMomentCommand;
        protected override void Initialize(INavigationParameters parameters)
        {
            imageData = parameters.GetValue<byte[]>("image");
            displayTime = parameters.GetValue<int>("momentDisplayTime");
        }

        public SendMomentViewModel(INavigationService navigationService, IDialogService dialogService, ILogger logger, IFriendService friendService, IMomentService momentService) : base(navigationService, dialogService, logger)
        {
            FriendService = friendService;
            MomentService = momentService;

            Title = Strings.SendMoment;

            Friends = FriendService.Friends;
        }

        [Reactive]public ObservableCollection<User> Friends { get; set; }

        public Command SendMomentCommand
        {
            get { return sendMomentCommand ?? (sendMomentCommand = new Command(async () => await ExecuteSendMomentCommand())); }
        }

        public async Task ExecuteSendMomentCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                DialogService.ShowLoading(Strings.SendingMoment);
                if (await ConnectivityService.IsConnected())
                {
                    var success = await SendImage();
                    DialogService.HideLoading();
                    if (success)
                    {
                        DialogService.ShowSuccess(Strings.MomentSent, 1);
                        await NavigationService.GoBackAsync();
                    }
                    else
                    {
                        DialogService.ShowError(Strings.ErrorOcurred);
                    }
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

        private async Task<bool> SendImage()
        {
            var recipients = new List<User>();
            await Task.Run(() => {
                foreach (var friend in Friends)
                {
                    if (friend.SendMoment)
                    {
                        recipients.Add(friend);
                    }

                    friend.SendMoment = false;
                }
            });

            return await MomentService.SendMoment(new MemoryStream(imageData), recipients, displayTime);
        }
    }
}