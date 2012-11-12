using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using ACAPackagesListener.API;
using Bll.AutoConsumerAnalisys;
using Lrrc.Sys.Data;


namespace ACAPackagesListener
{
    class Program
    {
        static Connection connection;
        static AdmPackages admPackages;
        static void Main(string[] args)
        {
            IListenToPackages packagesListener = new UDPPackagesListener(8888);
            packagesListener.onPackageReceived += packagesListener_onPackageReceived;
            packagesListener.StartListening();
            connection = new Connection(Globals.ConnectionString);
            admPackages = AdmPackages.Crear(ref connection);
            while (true)
            {
            }
        }

        static void packagesListener_onPackageReceived(object o, PackageReceivedEventArgs e)
        {
            Console.WriteLine("Received message");
            Console.WriteLine(e.sourceIp);
            Console.WriteLine(e.message);
            admPackages.Insertar(0,e.sourceIp,e.message,false,true);
            try{
                admPackages.Sincronizar();
                admPackages.GetTblLogic.Rows.Clear();
            }catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
