using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACAPackagesListener.API
{
    public delegate void PackageReceivedHandler(object o, PackageReceivedEventArgs e);

    public interface IListenToPackages
    {
        void StartListening();
        event PackageReceivedHandler onPackageReceived;
    }
}
