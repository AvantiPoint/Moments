using Moments.Services;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Moments.AWSBackend.Services
{
    class AwsAccountService : IAccountService
    {
        private IAwsClient Client { get; }

        public AwsAccountService(IAwsClient client)
        {
            Client = client;
        }

        public Account Account { get; set; }
        public string AuthenticationToken { get; set; }
        public bool ReadyToSignIn { get; }
        public User User { get; set; }

        public Task<bool> DeleteAccount()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Login()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Login(Account account)
        {
            var json = JsonConvert.SerializeObject(account);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "registration")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            using (var result = await Client.SendMessage(requestMessage))
            {
                return result.StatusCode == HttpStatusCode.OK;
            }
        }

        public async Task Register(Account account, User user)
        {
            var requestBody = new AccountRegistrationRequest
            {
                Account = account,
                User = user
            };
            var json = JsonConvert.SerializeObject(requestBody);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/registration")
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            using (await Client.SendMessage(requestMessage)) { }
        }

        public void SignOut()
        {
            throw new NotImplementedException();
        }
    }
}
