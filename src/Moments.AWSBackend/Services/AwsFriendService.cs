using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Moments.Services;

namespace Moments.AWSBackend.Services
{
    class AwsFriendService : IFriendService
    {
        public ObservableCollection<User> Friends { get; set; }
        public ObservableCollection<User> PendingFriends { get; set; }

        public Task<bool> AcceptFriendship(User friend)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateFriendship(string username)
        {
            throw new NotImplementedException();
        }

        public Task DenyFriendship(User friend)
        {
            throw new NotImplementedException();
        }

        public Task RefreshFriendsList()
        {
            throw new NotImplementedException();
        }

        public Task RefreshPendingFriendsList()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserIsAlreadyFriend(string friendUserId)
        {
            throw new NotImplementedException();
        }
    }
}
