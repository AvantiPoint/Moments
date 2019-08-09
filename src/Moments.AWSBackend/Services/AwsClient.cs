using System;
using System.Net.Http;
using System.Threading.Tasks;
using Moments.AWSBackend.Helpers;

namespace Moments.AWSBackend.Services
{
    public class AwsClient : IAwsClient
    {
        private IAwsConfig Config { get; }

        public AwsClient(IAwsConfig config) => Config = config;

        public async Task<HttpResponseMessage> SendMessage(HttpRequestMessage request)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Config.Url);
                return await client.SendAsync(request);
            }
        }
    }
}
