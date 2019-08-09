using Moments.AWSBackend.Helpers;

namespace Moments.Services
{
    class AwsConfig : IAwsConfig
    {
        public string Url => Helpers.Secrets.AwsBackendUri;
        public string ApiKey { get; }
    }
}
