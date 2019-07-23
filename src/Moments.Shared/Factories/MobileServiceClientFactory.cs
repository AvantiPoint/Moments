using System;
using System.Net.Http;
using Microsoft.WindowsAzure.MobileServices;

// Source: http://thirteendaysaweek.com/2013/12/13/xamarin-ios-and-authentication-in-windows-azure-mobile-services-part-iii-custom-authentication/
namespace Moments
{
	public static class MobileServiceClientFactory
	{
        public static MobileServiceClient CreateClient() =>
            CreateClient(Array.Empty<HttpMessageHandler>());

		public static MobileServiceClient CreateClient(params HttpMessageHandler[] handlers)
		{
            return new MobileServiceClient(Keys.ApplicationURL, handlers);
			//return new MobileServiceClient (Keys.ApplicationURL, Keys.ApplicationKey, handlers);
		}
	}
}