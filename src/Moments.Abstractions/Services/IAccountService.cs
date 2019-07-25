using System.Threading.Tasks;

namespace Moments.Services
{
    public interface IAccountService
    {
        Account Account { get; set; }
        string AuthenticationToken { get; set; }
        bool ReadyToSignIn { get; }
        User User { get; set; }

        Task<bool> DeleteAccount();
        Task<bool> Login();
        Task<bool> Login(Account account);
        Task Register(Account account, User user);
        void SignOut();
    }
}