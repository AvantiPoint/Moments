using System.Net.Http;
using Microsoft.WindowsAzure.MobileServices;
using Moments.AzureMobileApps.Helpers;

namespace Moments
{
    public interface IMobileServiceClientFactory
    {
        IDisposableMobileServiceClient CreateClient(params HttpMessageHandler[] handlers);
    }
}