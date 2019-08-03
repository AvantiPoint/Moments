using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Moments.Services;

namespace Moments.MockData.Services
{
    public class MockFriendService : IFriendService
    {
        public ObservableCollection<User> Friends { get; set; }
        public ObservableCollection<User> PendingFriends { get; set; }

        public Task<bool> AcceptFriendship(User friend)
        {
            return Task.FromResult(true);
        }

        public Task<bool> CreateFriendship(string username)
        {
            return Task.FromResult(true);
        }

        public Task DenyFriendship(User friend)
        {
            return Task.CompletedTask;
        }

        public Task RefreshFriendsList()
        {
            return Task.CompletedTask;
        }

        public Task RefreshPendingFriendsList()
        {
            return Task.CompletedTask;
        }

        public Task<bool> UserIsAlreadyFriend(string friendUserId)
        {
            return Task.FromResult(true);
        }
    }
}
