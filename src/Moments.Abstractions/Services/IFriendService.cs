using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Moments.Services
{
    public interface IFriendService
    {
        ObservableCollection<User> Friends { get; set; }
        ObservableCollection<User> PendingFriends { get; set; }

        Task<bool> AcceptFriendship(User friend);
        Task<bool> CreateFriendship(string username);
        Task DenyFriendship(User friend);
        Task RefreshFriendsList();
        Task RefreshPendingFriendsList();
        Task<bool> UserIsAlreadyFriend(string friendUserId);
    }
}