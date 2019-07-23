using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Moments.Views
{
	public partial class MomentsPage
	{
		public MomentsPage ()
		{
			BindingContext = new MomentsViewModel ();

			InitializeComponent ();
			SetupEventHandlers ();
		}

		private MomentsViewModel ViewModel
		{
			get { return BindingContext as MomentsViewModel; }
		}

		private void SetupEventHandlers ()
		{
			momentsListView.Refreshing += (sender, e) => {
				momentsListView.IsRefreshing = true;
				ViewModel.FetchMomentsCommand.Execute (null);
				momentsListView.IsRefreshing = false;
			};

			momentsListView.ItemSelected += (s, e) => {
				momentsListView.SelectedItem = null;
			};

			momentsListView.ItemTapped += (s, e) => {
				var moment = e.Item as Moment;
				if (moment == null) {
					return;
				}

				App.Current.MainPage.Navigation.PushModalAsync (new ViewMomentPage (moment.MomentUrl, TimeSpan.FromSeconds (moment.DisplayTime)));
				ViewModel.DestroyImageCommand.Execute (moment);
				momentsListView.SelectedItem = null;
			};
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			if (ViewModel == null || ViewModel.IsBusy || ViewModel.Moments.Count > 0) {
				return;
			}

			ViewModel.FetchMomentsCommand.Execute (null);
		}
	}
}

