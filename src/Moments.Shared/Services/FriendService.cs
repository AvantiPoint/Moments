using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Linq;

using Microsoft.WindowsAzure.MobileServices;
using Akavache;

namespace Moments
{
	public class FriendService
	{
		private static FriendService instance;

		public static FriendService Instance
		{
			get 
			{
				if (instance == null) 
				{
					instance = new FriendService ();
				}

				return instance;
			}
		}

		public ObservableCollection<User> Friends { get; set; }
		public ObservableCollection<User> PendingFriends { get; set; }

		private FriendService ()
		{
			Friends = new ObservableCollection<User> ();
			PendingFriends = new ObservableCollection<User> ();
		}

		public async Task RefreshFriendsList ()
		{
			using (var handler = new ZumoAuthHeaderHandler ()) 
			{
				using (var client = MobileServiceClientFactory.CreateClient (handler)) 
				{
					var dictionary = new Dictionary<string, string> ();
					dictionary.Add ("getFriends", "true");
					dictionary.Add ("userId", AccountService.Instance.Account.UserId);

					Friends.Clear ();
					var friends =  await client.InvokeApiAsync<List<User>> ("friends", System.Net.Http.HttpMethod.Get, dictionary);

					var sortedFriends = friends.OrderBy (user => user.Name).ToList ();
					sortedFriends.ForEach (user => Friends.Add (user));
				}
			}
		}

		public async Task RefreshPendingFriendsList ()
		{
			using (var handler = new ZumoAuthHeaderHandler ()) 
			{
				using (var client = MobileServiceClientFactory.CreateClient (handler)) 
				{
					var dictionary = new Dictionary<string, string> ();
					dictionary.Add ("getFriends", "false");
					dictionary.Add ("userId", AccountService.Instance.Account.UserId);

					PendingFriends.Clear ();
					var friends =  await client.InvokeApiAsync<List<User>> ("friends", System.Net.Http.HttpMethod.Get, dictionary);

					var sortedFriends = friends.OrderBy (user => user.Name).ToList ();
					sortedFriends.ForEach (user => PendingFriends.Add (user));
				}
			}
		}

		public async Task<bool> CreateFriendship (string username)
		{
			using (var handler = new ZumoAuthHeaderHandler ()) 
			{
				using (var client = MobileServiceClientFactory.CreateClient (handler)) 
				{
					var friendUserId = await GetUserId (username);
					var userExists = UserExists (friendUserId);
					if (!userExists) {
						return false;
					}

					var alreadyFriends = await UserIsAlreadyFriend (friendUserId);
					if (alreadyFriends) {
						return false;
					}

					var friendship = new Friendship {
						UserId = AccountService.Instance.User.Id,
						FriendId = friendUserId,
						Accepted = false
					};

					await client.GetTable<Friendship> ().InsertAsync (friendship);

					return true;
				}
			}
		}

		public async Task<bool> AcceptFriendship (User friend)
		{
			PendingFriends.Remove (friend);
			Friends.Add (friend);

			using (var handler = new ZumoAuthHeaderHandler ()) {
				using (var client = MobileServiceClientFactory.CreateClient (handler)) {
					var existingFriendshipList = await client.GetTable<Friendship> ()
						.Where (friendship => friendship.UserId == friend.Id)
						.Where (friendship => friendship.FriendId == AccountService.Instance.Account.UserId).Select (user => user.Id).ToListAsync ();

					if (existingFriendshipList.Count == 0) {
						return false;
					} else {
						var friendship = await client.GetTable<Friendship> ().LookupAsync (existingFriendshipList [0]);
						friendship.Accepted = true;
						await client.GetTable<Friendship> ().UpdateAsync (friendship);

						return true;
					}
				}
			}
		}

		public async Task DenyFriendship (User friend)
		{
			PendingFriends.Remove (friend);

			using (var handler = new ZumoAuthHeaderHandler ()) {
				using (var client = MobileServiceClientFactory.CreateClient (handler)) {
					var existingFriendshipList = await client.GetTable<Friendship> ()
						.Where (friendship => friendship.UserId == friend.Id)
						.Where (friendship => friendship.FriendId == AccountService.Instance.Account.UserId).ToListAsync ();

					var existingFriendship = existingFriendshipList [0];
					await client.GetTable<Friendship> ().DeleteAsync (existingFriendship);
				}
			}
		}
			
		private async Task<string> GetUserId (string username)
		{
			using (var handler = new ZumoAuthHeaderHandler ()) {
				using (var client = MobileServiceClientFactory.CreateClient (handler)) {
					var userId = await client.GetTable <Account> ().Where (account => account.Username == username)
						.Select (account => account.UserId).ToListAsync ();

					if (userId.Count != 0) {
						return userId [0];
					} else {
						return null;
					}
				}
			}
		}

		private bool UserExists (string userId)
		{
			if (string.IsNullOrEmpty (userId)) {
				return false;
			} else {
				return true;
			}
		}

		public async Task<bool> UserIsAlreadyFriend (string friendUserId)
		{
			using (var handler = new ZumoAuthHeaderHandler ()) {
				using (var client = MobileServiceClientFactory.CreateClient (handler)) {
					var friendships = client.GetTable <Friendship> ();
					var friend = await friendships.CreateQuery ().Where (friendship => friendship.UserId == AccountService.Instance.Account.UserId)
						.Where (friendship => friendship.FriendId == friendUserId).ToListAsync ();

					if (friend.Count == 0) {
						return false;
					} else {
						return true;
					}
				}
			}
		}
	}
}