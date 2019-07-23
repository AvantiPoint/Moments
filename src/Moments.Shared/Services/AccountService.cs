using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Crashes;
using Xamarin.Essentials.Interfaces;

// Source: http://thirteendaysaweek.com/2013/12/13/xamarin-ios-and-authentication-in-windows-azure-mobile-services-part-iii-custom-authentication/
namespace Moments
{
	public class AccountService
	{
		private static AccountService instance;

		public static AccountService Instance
		{
			get 
			{
				if (instance == null) 
				{
					instance = new AccountService (new Xamarin.Essentials.Implementation.PreferencesImplementation());
				}

				return instance;
			}
		}

		public string AuthenticationToken { get; set; }
		public Account Account { get; set; }
		public User User { get; set; }

		public bool ReadyToSignIn
		{
			get { return !string.IsNullOrEmpty (AuthenticationToken); }
		}

        private IPreferences Preferences { get; }

		public AccountService (IPreferences preferences)
		{
            Preferences = preferences;
			FetchAuthenticationToken ();
		}

		void FetchAuthenticationToken ()
		{
			var expiration = Preferences.Get("tokenExpiration", default(DateTime));
			if (expiration != null && DateTime.Compare (expiration, DateTime.Now) > 0) {
				AuthenticationToken = Preferences.Get("authenticationKey", string.Empty);
			}
		}

		public async Task Register (Account account, User user)
		{
			using (var client = MobileServiceClientFactory.CreateClient ()) {
				await client.GetTable<User> ().InsertAsync (user);
			}

			account.UserId = user.Id;

			await Insert (account, false);
		}

		public async Task<bool> Login ()
		{
			bool result = false;

			try
			{
				await BlobCache.Secure.Vacuum ();
				if (!string.IsNullOrEmpty (AuthenticationToken)) {
					var loginInfo = await BlobCache.Secure.GetLoginAsync ();
					var account = new Account {
						Username = loginInfo.UserName,
						Password = loginInfo.Password
					};

					await Insert (account, true);
					Account = await GetCurrentAccount (account);
					User = await GetCurrentUser ();

					await FriendService.Instance.RefreshFriendsList ();
					await FriendService.Instance.RefreshPendingFriendsList ();

                    var moreInformation = new CustomProperties();
                    moreInformation.Set("Name", User.Name);
                    moreInformation.Set("Email", Account.Email);
                    AppCenter.SetUserId(Account.Username);
                    AppCenter.SetCustomProperties(moreInformation);

					result = true;
				}
			}
			catch
			{
				result = false;
			}

			return result;
		}

		public async Task<bool> Login (Account account)
		{
			bool result;

			try
			{
				await Insert (account, true);
				Account = await GetCurrentAccount (account);
				User = await GetCurrentUser ();

				await FriendService.Instance.RefreshFriendsList ();
				await FriendService.Instance.RefreshPendingFriendsList ();

				await BlobCache.Secure.SaveLogin (account.Username, account.Password, "default", 
					DateTimeOffset.Now.AddDays (30));
				
				Preferences.Set("profileImage", User.ProfileImage);
				Preferences.Set("profileName", User.Name);

				// Store token / credentials
				await BlobCache.LocalMachine.InsertObject <string> ("authenticationToken", AccountService.Instance.AuthenticationToken,
					DateTimeOffset.Now.AddDays (30));

                var moreInformation = new CustomProperties();
                moreInformation.Set("Name", User.Name);
                moreInformation.Set("Email", Account.Email);
                AppCenter.SetUserId(Account.Username);
                AppCenter.SetCustomProperties(moreInformation);

                result = true;
			}
			catch (Exception ex)
			{
                Crashes.TrackError(ex);
				result = false;
			}

			return result;
		}

		public void SignOut ()
		{
			AccountService.Instance.AuthenticationToken = "";
			Preferences.Remove("authenticationKey");
			Preferences.Remove ("tokenExpiration");
		}

		public async Task<bool> DeleteAccount ()
		{
			bool result;

			try {
				using (var handler = new AuthenticationHandler ()) {
					using (var client = MobileServiceClientFactory.CreateClient (handler)) {
						// Account
						var accountTable = client.GetTable<Account> ();
						await accountTable.DeleteAsync (Account);

						// User
						var userTable = client.GetTable <User> ();
						await userTable.DeleteAsync (User);

						// Friendships
						var friendships = await client.GetTable<Friendship> ()
							.Where (friendship => friendship.UserId == AccountService.Instance.User.Id).Select (friendship => friendship).ToListAsync ();

						friendships.AddRange (await client.GetTable<Friendship> ()
							.Where (friendship => friendship.FriendId == AccountService.Instance.User.Id).Select (friendship => friendship).ToListAsync ());

						var friendshipsTable = client.GetTable<Friendship> ();
						foreach (var friend in friendships) {
							await friendshipsTable.DeleteAsync (friend);
						}

						// Moments
						var moments = await client.GetTable<Moment> ()
							.Where (moment => moment.SenderUserId == AccountService.Instance.User.Id).Select (moment => moment).ToListAsync ();

						moments.AddRange (await client.GetTable<Moment> ()
							.Where (moment => moment.RecipientUserId == AccountService.Instance.User.Id).Select (moment => moment).ToListAsync ());

						var momentsTable = client.GetTable<Moment> ();
						foreach (var moment in moments) {
							await momentsTable.DeleteAsync (moment);
						}
					}
				}

				result = true;
			} catch {
				result = false;
			}

			return result;
		}

		private async Task Insert (Account account, bool isLogin)
		{
			using (var handler = new AuthenticationHandler ()) {

				using (var client = MobileServiceClientFactory.CreateClient (handler)) {
					var table = client.GetTable<Account> ();
					var parameters = new Dictionary<string, string> {
						{ "login", isLogin.ToString ().ToLower ()}
					};

					await table.InsertAsync (account, parameters);
				}
			}
		}

		private async Task<Account> GetCurrentAccount (Account account)
		{
			using (var handler = new ZumoAuthHeaderHandler ()) {
				using (var client = MobileServiceClientFactory.CreateClient (handler)) {
					var currentAccount = await client.GetTable <Account> ()
						.Where (acct => acct.Username == account.Username)
						.Select (acct => acct).ToListAsync ();

					return currentAccount [0];
				}
			}
		}

		private async Task<User> GetCurrentUser ()
		{
			using (var handler = new ZumoAuthHeaderHandler ()) {
				using (var client = MobileServiceClientFactory.CreateClient (handler)) {
					return await client.GetTable <User> ().LookupAsync (Account.UserId);
				}
			}
		}
	}
}