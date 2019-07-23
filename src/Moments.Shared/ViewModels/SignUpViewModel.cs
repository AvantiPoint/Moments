using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Xamarin;
using Xamarin.Forms;

namespace Moments
{
	public class SignUpViewModel : BaseViewModel
	{
		string firstName;
		string lastName;
		string username;
		string password;
		string email;

		Command signUpUserCommand;

		public string FirstName
		{
			get { return firstName; }
			set { firstName = value; OnPropertyChanged ("FirstName"); }
		}

		public string LastName
		{
			get { return lastName; }
			set { lastName = value; OnPropertyChanged ("LastName"); }
		}

		public string Username
		{
			get { return username; }
			set { username = value; OnPropertyChanged ("Username"); }
		}

		public string Password
		{
			get { return password; }
			set { password = value; OnPropertyChanged ("Password"); }
		}

		public string Email 
		{
			get { return email; }
			set { email = value; OnPropertyChanged ("Email"); }
		}

		public Command SignUpUserCommand
		{
			get { return signUpUserCommand ?? (signUpUserCommand = new Command (async () => await ExecuteSignUpUserCommand ())); }
		}

		private async Task ExecuteSignUpUserCommand ()
		{
			if (IsBusy) {
				return;
			}

			IsBusy = true;

			var user = new User {
				Name = string.Format ("{0} {1}", FirstName, LastName),
				ProfileImage = GravatarService.CalculateUrl (Email)
			};

			var account = new Account {
				Username = Username,
				Password = Password,
				Email = Email,
				UserId = user.Id
			};
		
			try
			{
				DialogService.ShowLoading (Strings.CreatingAccount);
				if (await ConnectivityService.IsConnected ()) {
					await CreateAccount (account, user);

					await SignIn (account);
					NavigateToMainUI ();

					DialogService.HideLoading ();
				} else {
					DialogService.ShowError (Strings.NoInternetConnection);
				}
			}
			catch (Exception ex) 
			{
                Crashes.TrackError(ex);
			}

			IsBusy = false;
		}

		private async Task CreateAccount (Account account, User user)
		{
			await AccountService.Instance.Register (account, user);
		}

		private async Task SignIn (Account account)
		{
			await AccountService.Instance.Login (account);
		}

		private void NavigateToMainUI ()
		{
			App.Current.MainPage = App.FetchMainUI ();
		}
	}
}