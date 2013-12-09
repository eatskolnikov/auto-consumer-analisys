using System;
using System.Diagnostics;
using ACAPackagesListener.API.Models.Entities;
using ACAPackagesListener.API.Models.Repositories;
using ACAPackagesListener.API;
namespace ACAPackagesListener
{
    public class Program
    {
        private static ACAPackagesListener.API.Models.Repositories.IPackageRepository packagesRepository;
        public static void Main(string[] args)
        {
            IListenToPackages packagesListener = new UDPPackagesListener(8888);
            packagesListener.onPackageReceived += packagesListener_onPackageReceived;
            packagesListener.StartListening();
            while (true)
            {
            }
        }

        public static void packagesListener_onPackageReceived(object o, PackageReceivedEventArgs e)
        {
            Console.WriteLine("Received message");
            Console.WriteLine(e.sourceIp);
            Console.WriteLine(e.message);

            try
            {
//                packagesRepository.Add(new Package
//                {
//                    Ip = e.sourceIp,
//                    Message = e.message,
//                    Parsed = false
//                });
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
