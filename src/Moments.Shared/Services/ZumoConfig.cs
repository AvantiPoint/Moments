using System;
using System.Collections.Generic;
using System.Text;
using Moments.AzureMobileApps.Helpers;

namespace Moments.Services
{
    class ZumoConfig : IZumoConfig
    {
        public string ApplicationURL { get; }
        public string ContainerURL { get; }
        public string ContainerName { get; }
    }
}
