using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Essentials.Interfaces;

namespace Moments
{
	public class ConnectivityService
	{
        //private IConnectivity Connectivity { get; }

		public static Task<bool> IsConnected ()
		{
            return Task.FromResult(Connectivity.NetworkAccess == NetworkAccess.Internet);
			//return Connectivity.ConnectionProfiles.IsRemoteReachable (Keys.ApplicationMobileService, 80, 5000);
		}
	}
}

