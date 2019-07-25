using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Moments.Helpers;
using Moments.Mvvm;
using Moments.Services;
using Prism.Logging;
using Prism.Navigation;
using Prism.Services.Dialogs;
using ReactiveUI.Fody.Helpers;
using Xamarin;
using Xamarin.Forms;

namespace Moments.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {

        Command signUpUserCommand;
        private IAccountService AccountService { get; }

        public SignUpViewModel(INavigationService navigationService, IDialogService dialogService, ILogger logger, IAccountService accountService) : base(navigationService, dialogService, logger)
        {
            AccountService = accountService;
        }

        [Reactive] public string FirstName { get; set; }

        [Reactive] public string LastName { get; set; }

        [Reactive] public string Username { get; set; }

        [Reactive] public string Password { get; set; }

        [Reactive] public string Email { get; set; }

        public Command SignUpUserCommand
        {
            get { return signUpUserCommand ?? (signUpUserCommand = new Command(async () => await ExecuteSignUpUserCommand())); }
        }

        private async Task ExecuteSignUpUserCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            var user = new User
            {
                Name = string.Format("{0} {1}", FirstName, LastName),
                ProfileImage = GravatarService.CalculateUrl(Email)
            };

            var account = new Account
            {
                Username = Username,
                Password = Password,
                Email = Email,
                UserId = user.Id
            };

            try
            {
                DialogService.ShowLoading(Strings.CreatingAccount);
                if (await ConnectivityService.IsConnected())
                {
                    await CreateAccount(account, user);

                    await SignIn(account);
                    NavigateToMainUI();

                    DialogService.HideLoading();
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

        private async Task CreateAccount(Account account, User user)
        {
            await AccountService.Register(account, user);
        }

        private async Task SignIn(Account account)
        {
            await AccountService.Login(account);
        }

        private void NavigateToMainUI()
        {
            App.Current.MainPage = App.FetchMainUI();
        }
    }
}