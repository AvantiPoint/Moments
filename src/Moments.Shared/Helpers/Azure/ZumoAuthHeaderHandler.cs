using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

// Source: http://thirteendaysaweek.com/2013/12/13/xamarin-ios-and-authentication-in-windows-azure-mobile-services-part-iii-custom-authentication/
namespace Moments
{
	public class ZumoAuthHeaderHandler : DelegatingHandler
	{
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (string.IsNullOrWhiteSpace(AccountService.Instance.AuthenticationToken))
			{
				throw new InvalidOperationException("User is not currently logged in");
			}

			request.Headers.Add("X-ZUMO-AUTH", AccountService.Instance.AuthenticationToken);

			return base.SendAsync(request, cancellationToken);
		}
	}
}