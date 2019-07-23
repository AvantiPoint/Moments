using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Moments.Views
{
	public partial class SignInPage
	{
		TapGestureRecognizer cancelButtonTapped;

		public SignInPage ()
		{
			BindingContext = new SignInViewModel ();

			InitializeComponent ();
			SetupEventHandlers ();
		}

		private void SetupEventHandlers ()
		{
			usernameEntry.Completed += (sender, e) => {
				passwordEntry.Focus ();
			};

			cancelButtonTapped = new TapGestureRecognizer ();
			cancelButtonTapped.Tapped += (object sender, EventArgs e) => {
				Navigation.PopModalAsync ();
			};
			cancelButton.GestureRecognizers.Add (cancelButtonTapped);
		}
	}
}

