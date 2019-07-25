using System;
using System.Collections.Generic;
using System.Text;

namespace Moments.AzureMobileApps.Helpers
{
    public interface IZumoConfig
    {
        string ApplicationURL { get; }
        string ContainerURL { get; }
        string ContainerName { get; }
    }
}
