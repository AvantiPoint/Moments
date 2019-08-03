using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
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
    public class SignInViewModel : BaseViewModel
    {
        private IAccountService AccountService { get; }

        public SignInViewModel(INavigationService navigationService, IDialogService dialogService, IAccountService accountService, ILogger logger)
            : base(navigationService, dialogService, logger)
        {
            AccountService = accountService;
            
            SignInUserCommand = ReactiveCommand.CreateFromTask(ExecuteSignInUserCommand, SignInUserCommand.IsExecuting.Select(x => !x));
        }

        Command logInUserCommand;

        [Reactive]public string Username { get; set; }

        [Reactive]public string Password { get; set; }

        public ReactiveCommand<Unit,Unit> SignInUserCommand { get; }

        private async Task ExecuteSignInUserCommand()
        {
            IsBusy = true;

            try
            {
                DialogService.ShowLoading(Strings.SigningIn);
                if (await ConnectivityService.IsConnected())
                {
                    var result = await SignIn();
                    DialogService.HideLoading();

                    if (result)
                    {
                        NavigateToMainUI();
                    }
                    else
                    {
                        DialogService.ShowError(Strings.InvalidCredentials);
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

        private async Task<bool> SignIn()
        {
            var account = new Account
            {
                Username = Username,
                Password = Password
            };

            return await AccountService.Login(account);
        }

        private void NavigateToMainUI()
        {
            App.Current.MainPage = App.FetchMainUI();
        }
    }
}