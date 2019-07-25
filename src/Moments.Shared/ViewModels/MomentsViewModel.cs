using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Moments.Services;
using Prism.Logging;
using Xamarin.Forms;
using Prism.Navigation;
using Prism.Services.Dialogs;
using Moments.Helpers;
using Moments.Mvvm;
using ReactiveUI.Fody.Helpers;
using ReactiveUI;
using System.Reactive;

namespace Moments.ViewModels
{
    public class MomentsViewModel : BaseViewModel
    {
        private IMomentService MomentService { get; }

        private Command fetchMomentsCommand;
        private Command destroyImageCommand;

        public MomentsViewModel(INavigationService navigationService, IDialogService dialogService, IMomentService momentService, ILogger logger)
            : base(navigationService, dialogService, logger)
        {
            MomentService = momentService;
            Moments = new ObservableCollection<Moment>();
        }

        [Reactive]public ObservableCollection<Moment> Moments { get; set; }

        public ReactiveCommand<Moment, Unit> MomentTappedCommand { get; }

        private async Task OnMomentTappedCommandExecuted(Moment moment)
        {
            await NavigationService.NavigateAsync($"ViewMomentPage?{KnownNavigationParameters.UseModalNavigation}=true", new NavigationParameters
            {
                { "MomentViewTime", TimeSpan.FromSeconds (moment.DisplayTime) },
                { "Image", moment.MomentUrl }
            });

            await ExecuteDestroyImageCommand(moment);
        }

        public Command FetchMomentsCommand
        {
            get { return fetchMomentsCommand ?? (fetchMomentsCommand = new Command(async () => await ExecuteFetchMomentsCommand())); }
        }

        public Command DestroyImageCommand
        {
            get { return destroyImageCommand ?? (destroyImageCommand = new Command(async (object parameter) => await ExecuteDestroyImageCommand(parameter))); }
        }

        public async Task ExecuteFetchMomentsCommand()
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
                    Moments.Clear();
                    var refreshedMoments = await MomentService.GetMoments();
                    Moments.AddRange(refreshedMoments);
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

        public async Task ExecuteDestroyImageCommand(object parameter)
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                var moment = parameter as Moment;
                await DestroyImage(moment);
            }
            catch (Exception ex)
            {
                Logger.Report(ex);
            }

            IsBusy = false;
        }

        private async Task DestroyImage(Moment moment)
        {
            Moments.Remove(moment);

            await MomentService.DestroyMoment(moment);
        }

        protected override void OnAppearing()
        {
            FetchMomentsCommand.Execute(null);
        }
    }
}