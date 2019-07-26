using System;
using System.Net;
using Xamarin.Forms;
using Xamarin.Essentials.Interfaces;
using Moments.Mvvm;
using Moments.Helpers;
using Prism.Navigation;
using Prism.Services.Dialogs;
using Prism.Logging;
using Moments.Services;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;

namespace Moments.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private IPreferences Preferences { get; }
        private IAccountService AccountService { get; }

        public ProfileViewModel(INavigationService navigationService, IDialogService dialogService, ILogger logger, IPreferences preferences, IAccountService accountService) : base(navigationService, dialogService, logger)
        {
            Preferences = preferences;
            AccountService = accountService;

            SignOutCommand = ReactiveCommand.CreateFromTask(OnSignOutCommandExecuted);
            DeleteAccountCommand = ReactiveCommand.CreateFromTask(OnDeleteAccountCommandExecuted);
        }

        public string ProfileName => Preferences.Get("profileName", string.Empty);

        public string ProfileImageUrl
        {
            get => Preferences.Get("profileImage", string.Empty);
        }

        public ReactiveCommand<Unit, Unit> SignOutCommand { get; }

        public ReactiveCommand<Unit, Unit> DeleteAccountCommand { get; }

        private async Task OnSignOutCommandExecuted()
        {
            AccountService.SignOut();
            await NavigationService.NavigateAsync("/WelcomePage");
        }

        private async Task OnDeleteAccountCommandExecuted()
        {
            await AccountService.DeleteAccount();
            await NavigationService.NavigateAsync("/WelcomePage");
        }
    }
}