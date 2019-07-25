using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Eventing;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;

namespace Moments.AzureMobileApps.Helpers
{
    internal class DisposibleClient : IDisposableMobileServiceClient
    {
        private IMobileServiceClient Client { get; }

        public DisposibleClient(IMobileServiceClient client)
        {
            Client = client;
        }
        public Uri MobileAppUri => Client.MobileAppUri;
        public MobileServiceUser CurrentUser
        {
            get => Client.CurrentUser;
            set => Client.CurrentUser = value;
        }

        public string LoginUriPrefix
        {
            get => Client.LoginUriPrefix;
            set => Client.LoginUriPrefix = value;
        }

        public Uri AlternateLoginHost
        {
            get => Client.AlternateLoginHost;
            set => Client.AlternateLoginHost = value;
        }

        public IMobileServiceSyncContext SyncContext => Client.SyncContext;
        public string InstallationId => Client.InstallationId;
        public IMobileServiceEventManager EventManager => Client.EventManager;

        public MobileServiceJsonSerializerSettings SerializerSettings
        {
            get => Client.SerializerSettings;
            set => Client.SerializerSettings = value;
        }

        public IMobileServiceTable GetTable(string tableName) => Client.GetTable(tableName);

        public IMobileServiceSyncTable GetSyncTable(string tableName) =>
            Client.GetSyncTable(tableName);

        public IMobileServiceTable<T> GetTable<T>() => Client.GetTable<T>();

        public IMobileServiceSyncTable<T> GetSyncTable<T>() => Client.GetSyncTable<T>();

        public Task<MobileServiceUser> LoginAsync(MobileServiceAuthenticationProvider provider, JObject token) =>
            Client.LoginAsync(provider, token);

        public Task<MobileServiceUser> LoginAsync(string provider, JObject token) =>
            Client.LoginAsync(provider, token);

        public Task LogoutAsync() => Client.LogoutAsync();

        public Task<MobileServiceUser> RefreshUserAsync() => Client.RefreshUserAsync();

        public Task<T> InvokeApiAsync<T>(string apiName, CancellationToken cancellationToken = default) => Client.InvokeApiAsync<T>(apiName, cancellationToken);

        public Task<U> InvokeApiAsync<T, U>(string apiName, T body, CancellationToken cancellationToken = default) => Client.InvokeApiAsync<T, U>(apiName, body, cancellationToken);

        public Task<T> InvokeApiAsync<T>(string apiName, HttpMethod method, IDictionary<string, string> parameters, CancellationToken cancellationToken = default) =>
            Client.InvokeApiAsync<T>(apiName, method, parameters, cancellationToken);

        public Task<U> InvokeApiAsync<T, U>(string apiName, T body, HttpMethod method, IDictionary<string, string> parameters, CancellationToken cancellationToken = default) =>
            Client.InvokeApiAsync<T, U>(apiName, body, method, parameters, cancellationToken);

        public Task<JToken> InvokeApiAsync(string apiName, CancellationToken cancellationToken = default) =>
            Client.InvokeApiAsync(apiName, cancellationToken);

        public Task<JToken> InvokeApiAsync(string apiName, JToken body, CancellationToken cancellationToken = default) =>
            Client.InvokeApiAsync(apiName, body, cancellationToken);

        public Task<JToken> InvokeApiAsync(string apiName, HttpMethod method, IDictionary<string, string> parameters, CancellationToken cancellationToken = default) =>
            Client.InvokeApiAsync(apiName, method, parameters, cancellationToken);

        public Task<JToken> InvokeApiAsync(string apiName, JToken body, HttpMethod method, IDictionary<string, string> parameters, CancellationToken cancellationToken = default) =>
            Client.InvokeApiAsync(apiName, body, method, parameters, cancellationToken);

        public Task<HttpResponseMessage> InvokeApiAsync(string apiName, HttpContent content, HttpMethod method, IDictionary<string, string> requestHeaders, IDictionary<string, string> parameters, CancellationToken cancellationToken = default) =>
            Client.InvokeApiAsync(apiName, content, method, requestHeaders, parameters, cancellationToken);

        public void Dispose()
        {
            if(Client is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
