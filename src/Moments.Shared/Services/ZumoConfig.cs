using Moments.AzureMobileApps.Helpers;
using Moments.Helpers;

namespace Moments.Services
{
    class ZumoConfig : IZumoConfig
    {
        public string ApplicationURL => Secrets.ApplicationURL;
        public string ContainerURL => Secrets.ContainerURL;
        public string ContainerName => Secrets.ContainerName;
    }
}
