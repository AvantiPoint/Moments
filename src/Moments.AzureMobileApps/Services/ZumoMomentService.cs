using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Prism.Logging;
using Moments.Services;
using Moments.AzureMobileApps.Helpers.Azure;
using Moments.AzureMobileApps.Helpers;

namespace Moments.AzureMobileApps.Services
{
    public class ZumoMomentService : IMomentService
    {
        private IAccountService AccountService { get; }
        private ILogger Logger { get; }
        private IZumoConfig Config { get; }
        private IMobileServiceClientFactory MobileServiceClientFactory { get; }

        public ZumoMomentService(IAccountService accountService, ILogger logger, IZumoConfig config, IMobileServiceClientFactory factory)
        {
            AccountService = accountService;
            Logger = logger;
            Config = config;
            MobileServiceClientFactory = factory;
        }

        public async Task<List<Moment>> GetMoments()
        {
            using (var handler = new ZumoAuthHeaderHandler(AccountService))
            {
                using (var client = MobileServiceClientFactory.CreateClient(handler))
                {
                    var friendships = client.GetTable<Moment>();
                    var moments = await friendships.CreateQuery().Where(moment => moment.RecipientUserId == AccountService.Account.UserId).Select(moment => moment).ToListAsync();

                    return moments.OrderByDescending(moment => moment.TimeSent).ToList();
                }
            }
        }

        public async Task<bool> SendMoment(Stream image, List<User> recipients, int displayTime)
        {
            bool result;

            try
            {
                var blob = await SaveMoment(image);
                var imageUrl = blob.Uri.ToString();

                await SendImageToUsers(imageUrl, recipients, displayTime);

                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public async Task DestroyMoment(Moment moment)
        {
            using (var handler = new ZumoAuthHeaderHandler(AccountService))
            {
                using (var client = MobileServiceClientFactory.CreateClient(handler))
                {
                    await client.GetTable<Moment>().DeleteAsync(moment);
                }
            }
        }

        private async Task<CloudBlockBlob> SaveMoment(Stream image)
        {
            var blobName = string.Format("{0}{1}", DateTime.Now.ToString(), Guid.NewGuid().ToString()).ToLower();

            var sas = await FetchSas();
            var credentials = new StorageCredentials(sas);

            var container = new CloudBlobContainer(new Uri(Config.ContainerURL), credentials);
            var blob = container.GetBlockBlobReference(blobName);
            await blob.UploadFromStreamAsync(image);

            Logger.TrackEvent("ImageUploaded");

            return blob;
        }

        private async Task<string> FetchSas()
        {
            using (var handler = new ZumoAuthHeaderHandler(AccountService))
            {
                using (var client = MobileServiceClientFactory.CreateClient(handler))
                {
                    var dictionary = new Dictionary<string, string>
                    {
                        { "containerName", Config.ContainerName }
                    };

                    return await client.InvokeApiAsync<string>("sas", System.Net.Http.HttpMethod.Get, dictionary);
                }
            }
        }

        private async Task SendImageToUsers(string imageUrl, List<User> recipients, int displayTime)
        {
            using (var handler = new ZumoAuthHeaderHandler(AccountService))
            {
                using (var client = MobileServiceClientFactory.CreateClient(handler))
                {
                    var senderUserId = AccountService.User.Id;
                    var senderProfileImage = AccountService.User.ProfileImage;
                    var senderName = AccountService.User.Name;
                    var timeSent = DateTime.UtcNow;

                    foreach (var user in recipients)
                    {
                        var recipientUserId = user.Id;

                        var moment = new Moment
                        {
                            MomentUrl = imageUrl,
                            SenderUserId = senderUserId,
                            SenderName = senderName,
                            SenderProfileImage = senderProfileImage,
                            RecipientUserId = recipientUserId,
                            DisplayTime = displayTime,
                            TimeSent = timeSent
                        };

                        Logger.TrackEvent("MomentShared");

                        await client.GetTable<Moment>().InsertAsync(moment);
                    }
                }
            }
        }
    }
}