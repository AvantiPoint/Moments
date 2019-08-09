using System.Net.Http;
using System.Threading.Tasks;

namespace Moments.AWSBackend.Services
{
    public interface IAwsClient
    {
        Task<HttpResponseMessage> SendMessage(HttpRequestMessage request);
    }
}