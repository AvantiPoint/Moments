using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.AppCenter.Analytics;

namespace Moments
{
	public class MomentService
	{
		private static MomentService instance;

		public static MomentService Instance
		{
			get 
			{
				if (instance == null) 
				{
					instance = new MomentService ();
				}

				return instance;
			}
		}

		public async Task<List<Moment>> GetMoments ()
		{
			using (var handler = new ZumoAuthHeaderHandler ()) {
				using (var client = MobileServiceClientFactory.CreateClient (handler)) {
					var friendships = client.GetTable <Moment> ();
					var moments = await friendships.CreateQuery ().Where (moment => moment.RecipientUserId == AccountService.Instance.Account.UserId).Select (moment => moment).ToListAsync ();

					return moments.OrderByDescending (moment => moment.TimeSent).ToList ();
				}
			}
		}

		public async Task<bool> SendMoment (Stream image, List<User> recipients, int displayTime)
		{
			bool result;

			try {
				var blob = await SaveMoment (image);
				var imageUrl = blob.Uri.ToString ();

				await SendImageToUsers (imageUrl, recipients, displayTime);

				result = true;
			} catch {
				result = false;
			}

			return result;
		}

		public async Task DestroyMoment (Moment moment)
		{
			using (var handler = new ZumoAuthHeaderHandler ()) 
			{
				using (var client = MobileServiceClientFactory.CreateClient (handler)) 
				{
					await client.GetTable<Moment> ().DeleteAsync (moment);
				}
			}
		}

		private static async Task<CloudBlockBlob> SaveMoment (Stream image)
		{
			var blobName = string.Format ("{0}{1}", DateTime.Now.ToString (), Guid.NewGuid ().ToString ()).ToLower ();

			var sas = await FetchSas ();
			var credentials = new StorageCredentials (sas);

			var container = new CloudBlobContainer (new Uri (Keys.ContainerURL), credentials);
			var blob = container.GetBlockBlobReference (blobName);
			await blob.UploadFromStreamAsync (image);

            Analytics.TrackEvent("ImageUploaded");

			return blob;
		}

		private static async Task<string> FetchSas ()
		{
			using (var handler = new ZumoAuthHeaderHandler ()) 
			{
				using (var client = MobileServiceClientFactory.CreateClient (handler)) 
				{
					var dictionary = new Dictionary<string, string> ();
					dictionary.Add ("containerName", Keys.ContainerName);

					return await client.InvokeApiAsync<string> ("sas", System.Net.Http.HttpMethod.Get, dictionary);
				}
			}
		}

		private async Task SendImageToUsers (string imageUrl, List<User> recipients, int displayTime)
		{
			using (var handler = new ZumoAuthHeaderHandler ()) {
				using (var client = MobileServiceClientFactory.CreateClient (handler)) {
					var senderUserId = AccountService.Instance.User.Id;
					var senderProfileImage = AccountService.Instance.User.ProfileImage;
					var senderName = AccountService.Instance.User.Name;
					var timeSent = DateTime.UtcNow;

					foreach (var user in recipients) {
						var recipientUserId = user.Id;

						var moment = new Moment {
							MomentUrl = imageUrl,
							SenderUserId = senderUserId,
							SenderName = senderName,
							SenderProfileImage = senderProfileImage,
							RecipientUserId = recipientUserId,
							DisplayTime = displayTime,
							TimeSent = timeSent
						};

                        Analytics.TrackEvent("MomentShared");

						await client.GetTable<Moment> ().InsertAsync (moment);
					}
				}
			}
		}
	}
}