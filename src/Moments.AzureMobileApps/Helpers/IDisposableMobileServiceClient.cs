using System;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;

namespace Moments.AzureMobileApps.Helpers
{
    public interface IDisposableMobileServiceClient : IMobileServiceClient, IDisposable
    {
    }
}
