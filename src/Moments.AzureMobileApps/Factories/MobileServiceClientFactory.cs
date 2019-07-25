using System.Net.Http;
using Microsoft.WindowsAzure.MobileServices;
using Moments.AzureMobileApps.Helpers;
using Prism.Ioc;

// Source: http://thirteendaysaweek.com/2013/12/13/xamarin-ios-and-authentication-in-windows-azure-mobile-services-part-iii-custom-authentication/
namespace Moments
{
    public class MobileServiceClientFactory : IMobileServiceClientFactory
    {
        private IZumoConfig Config { get; }
        private IContainerProvider Container { get; }

        public MobileServiceClientFactory(IZumoConfig config, IContainerExtension container)
        {
            Config = config;
            Container = container;
        }

        public IDisposableMobileServiceClient CreateClient(params HttpMessageHandler[] handlers)
        {
            var client = Container.Resolve<IMobileServiceClient>((typeof(string), Config.ApplicationURL), (typeof(HttpMessageHandler[]), handlers));

            return new DisposibleClient(client);
        }
    }
}