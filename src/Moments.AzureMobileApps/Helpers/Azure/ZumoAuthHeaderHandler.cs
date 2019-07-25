using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moments.Services;

// Source: http://thirteendaysaweek.com/2013/12/13/xamarin-ios-and-authentication-in-windows-azure-mobile-services-part-iii-custom-authentication/
namespace Moments.AzureMobileApps.Helpers.Azure
{
    public class ZumoAuthHeaderHandler : DelegatingHandler
    {
        IAccountService AccountService { get; }

        public ZumoAuthHeaderHandler(IAccountService accountService)
        {
            AccountService = accountService;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(AccountService.AuthenticationToken))
            {
                throw new InvalidOperationException("User is not currently logged in");
            }

            request.Headers.Add("X-ZUMO-AUTH", AccountService.AuthenticationToken);

            return base.SendAsync(request, cancellationToken);
        }
    }
}