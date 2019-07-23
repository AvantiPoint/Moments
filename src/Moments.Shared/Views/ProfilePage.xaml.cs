using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Moments.Views
{
	public partial class ProfilePage
	{
		public ProfilePage ()
		{
			BindingContext = new ProfileViewModel ();

			InitializeComponent ();
			SetupEventHandlers ();
		}

		private void SetupEventHandlers ()
		{
			signOutButton.Clicked += (sender, e) => {
				AccountService.Instance.SignOut ();
				App.Current.MainPage = new WelcomePage ();
			};

			deleteAccountButton.Clicked += async (sender, e) => {
				await AccountService.Instance.DeleteAccount ();
				App.Current.MainPage = new WelcomePage ();
			};

			aboutMomentsButton.Clicked += async (sender, e) => {
				var aboutPage = new AboutPage ();
				await Navigation.PushAsync (aboutPage);
			};
		}
	}
}

