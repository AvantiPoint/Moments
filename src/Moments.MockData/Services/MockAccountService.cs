using System;
using System.Threading.Tasks;
using Moments.Services;

namespace Moments.MockData.Services
{
    public class MockAccountService : IAccountService
    {
        public Account Account { get; set; }
        public string AuthenticationToken { get; set; }

        public bool ReadyToSignIn => false;

        public User User { get; set; }

        public Task<bool> DeleteAccount()
        {
            return Task.FromResult(true);
        }

        public Task<bool> Login()
        {
            return Task.FromResult(true);
        }

        public Task<bool> Login(Account account)
        {
            var success = account.Username.Equals("prism", StringComparison.InvariantCultureIgnoreCase) && account.Password.Equals("rocks", StringComparison.InvariantCultureIgnoreCase);

            if(success)
            {
                Account = account;
                Account.Email = "prismrocks@gmail.com";
                Account.Id = "prismrocks";
                Account.UserId = "foobar";
            }

            return Task.FromResult(success);
        }

        public Task Register(Account account, User user)
        {
            return Task.CompletedTask;
        }

        public void SignOut()
        {

        }
    }
}
