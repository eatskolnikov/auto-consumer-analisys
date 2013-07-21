using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using ACAPackagesListener.API;
using ACAPackagesListener.API.Models.Entities;
using ACAPackagesListener.API.Models.Repositories;

namespace ACAPackagesListener
{
    class Program
    {
        private static IPackageRepository packagesRepository;
        static void Main(string[] args)
        {
            IListenToPackages packagesListener = new UDPPackagesListener(8888);
            packagesListener.onPackageReceived += packagesListener_onPackageReceived;
            packagesListener.StartListening();
            while (true)
            {
            }
        }

        static void packagesListener_onPackageReceived(object o, PackageReceivedEventArgs e)
        {
            Console.WriteLine("Received message");
            Console.WriteLine(e.sourceIp);
            Console.WriteLine(e.message);

            try
            {
                packagesRepository.Add(new Package
                {
                    Ip = e.sourceIp,
                    Message = e.message,
                    Parsed = false
                });
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
