using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Moments.Services;

namespace Moments.MockData.Services
{
    public class MockMomentService : IMomentService
    {
        private readonly List<Moment> Moments = new List<Moment>();

        public Task DestroyMoment(Moment moment)
        {
            return Task.CompletedTask;
        }

        public Task<List<Moment>> GetMoments() => Task.FromResult(Moments);

        public Task<bool> SendMoment(Stream image, List<User> recipients, int displayTime)
        {
            return Task.FromResult(true);
        }
    }
}
