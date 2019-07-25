using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Akavache;
using System.Reactive.Linq;
using System.Reactive;
using Xamarin.Essentials.Interfaces;
using Moments.Services;

// Source: http://thirteendaysaweek.com/2013/12/13/xamarin-ios-and-authentication-in-windows-azure-mobile-services-part-iii-custom-authentication/
namespace Moments.AzureMobileApps.Helpers.Azure
{
	public class AuthenticationHandler : DelegatingHandler
	{
        private IAccountService AccountService { get; }
        private IPreferences Preferences { get; }

        public AuthenticationHandler(IPreferences preferences, IAccountService accountService)
        {
            Preferences = preferences;
            AccountService = accountService;
        }

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var response = await base.SendAsync(request, cancellationToken);
			response.EnsureSuccessStatusCode();

			var jsonString = await response.Content.ReadAsStringAsync();
			var jsonObject = JObject.Parse(jsonString);
			var token = jsonObject["token"].ToString();
			SaveAuthenticationToken (token);

			return response;
		}

		private void SaveAuthenticationToken (string token)
		{
			AccountService.AuthenticationToken = token;
			Preferences.Set("authenticationKey", token);
			Preferences.Set("tokenExpiration", DateTime.Now.AddDays (30));
		}
	}
}