using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Moments.Views
{
	public partial class WelcomePage : ContentPage
	{
		public WelcomePage ()
		{
			InitializeComponent ();

			SetupUserInterface ();
			SetupEventHandlers ();
		}

		private void SetupUserInterface ()
		{
			NavigationPage.SetHasNavigationBar (this, false);
		}

		private void SetupEventHandlers ()
		{
			signUpButton.Clicked += (object sender, EventArgs e) => {
				Navigation.PushModalAsync (new SignUpPage ());
			};

			signInButton.Clicked += (object sender, EventArgs e) => {
				Navigation.PushModalAsync (new SignInPage ());
			};
		}
	}
}

