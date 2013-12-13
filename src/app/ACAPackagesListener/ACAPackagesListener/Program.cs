using System;
using System.Collections.Generic;
using System.Diagnostics;
using ACAPackagesListener.API.Models.Entities;
using ACAPackagesListener.API.Models.Repositories;
using ACAPackagesListener.API;
namespace ACAPackagesListener
{
    public class Program
    {
        private static ACAPackagesListener.API.Models.Repositories.IPackageRepository packagesRepository;
        private static Dictionary<string,DateTime> _lastPackages;
        public static void Main(string[] args)
        {
            _lastPackages = new Dictionary<string, DateTime>();
            packagesRepository = new NHPackageRepository();
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

            var nextPackage = new Package
                {
                    Ip = e.sourceIp,
                    Message = e.message,
                    Parsed = false
                };
            if (!_lastPackages.ContainsKey(nextPackage.Message))
            {
                _lastPackages.Add(nextPackage.Message, DateTime.Now);
            }
            else
            {
                var diffInSeconds = (DateTime.Now -_lastPackages[nextPackage.Message]).TotalSeconds;
                if (diffInSeconds < 5)
                {
                    Console.WriteLine("Paquete rechazado, solo han pasado {0} segundos desde el ultimo.", diffInSeconds);
                    return;
                }
            }
            try
            {
                Console.WriteLine("Insertando");
                _lastPackages[nextPackage.Message] = DateTime.Now;
                packagesRepository.Add(nextPackage);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
