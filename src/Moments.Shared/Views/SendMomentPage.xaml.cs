using System;
using System.Collections.Generic;
using System.IO;

using Xamarin.Forms;

namespace Moments.Views
{
	public partial class SendMomentPage
	{
		public SendMomentPage (Stream image, int displayTime)
		{
			BindingContext = new SendMomentViewModel (this, image, displayTime);

			InitializeComponent ();

			SetupToolbar ();
			SetupEventHandlers ();
		}

		private SendMomentViewModel ViewModel
		{
			get { return BindingContext as SendMomentViewModel; }
		}

		private void SetupToolbar ()
		{
			ToolbarItems.Add (new ToolbarItem {
				Text = Strings.Send,
				Priority = 1,
				Command = ViewModel.SendMomentCommand
			});
		}
		 
		private void SetupEventHandlers ()
		{
			friendsListView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) => {
				friendsListView.SelectedItem = null;
			};
		}
	}
}