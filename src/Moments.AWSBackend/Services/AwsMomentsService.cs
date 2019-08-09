using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Moments.Services;

namespace Moments.AWSBackend.Services
{
    class AwsMomentsService : IMomentService
    {
        public Task DestroyMoment(Moment moment)
        {
            throw new NotImplementedException();
        }

        public Task<List<Moment>> GetMoments()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendMoment(Stream image, List<User> recipients, int displayTime)
        {
            throw new NotImplementedException();
        }
    }
}
