using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACAPackagesListener.API;

namespace ACAPackagesListener
{
    class Program
    {
        static void Main(string[] args)
        {
            IListenToPackages packagesListener = new UDPPackagesListener(8888);
            packagesListener.onPackageReceived += new PackageReceivedHandler(packagesListener_onPackageReceived);
            packagesListener.StartListening();
            while (true) ;
        }

        static void packagesListener_onPackageReceived(object o, PackageReceivedEventArgs e)
        {
            Console.WriteLine("Received message");
            Console.WriteLine(e.sourceIp);
            Console.WriteLine(e.message);
        }
    }
}
