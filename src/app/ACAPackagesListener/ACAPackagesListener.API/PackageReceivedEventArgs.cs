using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACAPackagesListener.API
{
    public class PackageReceivedEventArgs : EventArgs
    {
        public string message { get; private set; }
        public string sourceIp { get; private set; }

        public PackageReceivedEventArgs(string sourceIp, string message)
        {
            this.message = message;
            this.sourceIp = sourceIp;
        }
    }
}
