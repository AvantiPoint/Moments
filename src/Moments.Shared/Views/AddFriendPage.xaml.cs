using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Moments.Views
{
	public partial class AddFriendPage : ContentPage
	{
		TapGestureRecognizer cancelButtonTapped;

		public AddFriendPage ()
		{
			BindingContext = new AddFriendViewModel ();

			InitializeComponent ();
			SetupEventHandlers ();
		}

		private void SetupEventHandlers ()
		{
			cancelButtonTapped = new TapGestureRecognizer ();
			cancelButtonTapped.Tapped += (object sender, EventArgs e) => {
				Navigation.PopModalAsync ();
			};
			cancelButton.GestureRecognizers.Add (cancelButtonTapped);
		}
	}
}

