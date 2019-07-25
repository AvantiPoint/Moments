using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Moments.Services
{
    public interface IMomentService
    {
        Task DestroyMoment(Moment moment);
        Task<List<Moment>> GetMoments();
        Task<bool> SendMoment(Stream image, List<User> recipients, int displayTime);
    }
}