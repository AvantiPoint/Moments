using Moments.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Moments.AWSBackend.Services
{
    class AwsAccountService : IAccountService
    {
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

        public Task<bool> Login(Account account)
        {
            throw new NotImplementedException();
        }

        public Task Register(Account account, User user)
        {
            throw new NotImplementedException();
        }

        public void SignOut()
        {
            throw new NotImplementedException();
        }
    }
}
