using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;

namespace Moments
{
	public class MomentsViewModel : BaseViewModel
	{
		private ObservableCollection<Moment> moments;

		private Command fetchMomentsCommand;
		private Command destroyImageCommand;

		public MomentsViewModel ()
		{
			moments = new ObservableCollection<Moment> ();
		}

		public ObservableCollection<Moment> Moments
		{
			get { return moments; }
			set { moments = value; OnPropertyChanged ("Moments"); }
		}

		public Command FetchMomentsCommand
		{
			get { return fetchMomentsCommand ?? (fetchMomentsCommand = new Command (async () => await ExecuteFetchMomentsCommand ())); }
		}

		public Command DestroyImageCommand
		{
			get { return destroyImageCommand ?? (destroyImageCommand = new Command (async (object parameter) => await ExecuteDestroyImageCommand (parameter))); }
		}

		public async Task ExecuteFetchMomentsCommand ()
		{
			if (IsBusy) {
				return;
			}

			IsBusy = true;

			try
			{
				if (await ConnectivityService.IsConnected ()) {
					Moments.Clear ();
					var refreshedMoments = await MomentService.Instance.GetMoments ();
					Moments.AddRange (refreshedMoments);
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

		public async Task ExecuteDestroyImageCommand (object parameter)
		{
			if (IsBusy) {
				return;
			}

			IsBusy = true;

			try
			{
				var moment = parameter as Moment;
				await DestroyImage (moment);
			}
			catch (Exception ex) 
			{
                Crashes.TrackError(ex);
			}

			IsBusy = false;
		}

		private async Task DestroyImage (Moment moment)
		{
			Moments.Remove (moment);

			await MomentService.Instance.DestroyMoment (moment);
		}
	}
}