using System;
using System.Net.Http;
using Microsoft.WindowsAzure.MobileServices;
using Moments.AzureMobileApps.Helpers;

// Source: http://thirteendaysaweek.com/2013/12/13/xamarin-ios-and-authentication-in-windows-azure-mobile-services-part-iii-custom-authentication/
namespace Moments
{
    public static class IMobileServiceClientFactoryExtensions
    {
        public static IDisposableMobileServiceClient CreateClient(this IMobileServiceClientFactory factory) =>
            factory.CreateClient(Array.Empty<HttpMessageHandler>());
    }
}