using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Moments.Views
{
	public partial class SignUpPage : ContentPage
	{
		TapGestureRecognizer cancelButtonTapped;

		public SignUpPage ()
		{
			BindingContext = new SignUpViewModel ();

			InitializeComponent ();
			SetupEventHandlers ();
		}

		private void SetupEventHandlers ()
		{
			firstNameEntry.Completed += (object sender, EventArgs e) => {
				lastNameEntry.Focus ();
			};

			lastNameEntry.Completed += (object sender, EventArgs e) => {
				usernameEntry.Focus ();
			};

			usernameEntry.Completed += (object sender, EventArgs e) => {
				passwordEntry.Focus ();
			};

			passwordEntry.Completed += (object sender, EventArgs e) => {
				emailEntry.Focus ();
			};

			cancelButtonTapped = new TapGestureRecognizer ();
			cancelButtonTapped.Tapped += (object sender, EventArgs e) => {
				Navigation.PopModalAsync ();
			};
			cancelButton.GestureRecognizers.Add (cancelButtonTapped);
		}
	}
}

