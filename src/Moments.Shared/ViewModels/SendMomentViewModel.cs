using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;

namespace Moments
{
	public class SendMomentViewModel : BaseViewModel
	{
		Stream imageData;
		int displayTime;
		ObservableCollection<User> friends;
		Page page;

		Command sendMomentCommand;

		public SendMomentViewModel (Page sendMomentPage, Stream image, int momentDisplayTime)
		{
			Title = Strings.SendMoment;

			friends = FriendService.Instance.Friends;
			imageData = image;
			displayTime = momentDisplayTime;
			page = sendMomentPage;
		}

		public ObservableCollection<User> Friends
		{
			get { return friends; }
			set { friends = value; OnPropertyChanged ("Friends"); }
		}

		public Command SendMomentCommand
		{
			get { return sendMomentCommand ?? (sendMomentCommand = new Command (async () => await ExecuteSendMomentCommand ())); }
		}

		public async Task ExecuteSendMomentCommand ()
		{
			if (IsBusy) {
				return;
			}

			IsBusy = true;

			try 
			{
				DialogService.ShowLoading (Strings.SendingMoment);
				if (await ConnectivityService.IsConnected ()) {
					var success = await SendImage ();
					DialogService.HideLoading ();
					if (success) {
						DialogService.ShowSuccess (Strings.MomentSent, 1);
						await page.Navigation.PopModalAsync ();
					} else {
						DialogService.ShowError (Strings.ErrorOcurred);
					}
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

		private async Task<bool> SendImage ()
		{
			var recipients = new List<User> ();
			await Task.Run (() => {
				foreach (var friend in Friends) {
					if (friend.SendMoment) {
						recipients.Add (friend);
					}

					friend.SendMoment = false;
				}
			});

			return await MomentService.Instance.SendMoment (imageData, recipients, displayTime);
		}
	}
}