using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Threading;
using System.ComponentModel;

namespace ACAPackagesListener.API
{
    public class UDPPackagesListener : IListenToPackages
    {
        private UdpClient udpClient;
        private IPEndPoint groupEP;
        private BackgroundWorker backgroundWorker;

        public UDPPackagesListener(int port)
        {
            udpClient = new UdpClient(port);
            groupEP = new IPEndPoint(IPAddress.Any, port);
            backgroundWorker = new BackgroundWorker();
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
        }

        public void StartListening()
        {
            this.backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var bw = sender as BackgroundWorker;
            listen(bw);
        }
        private void listen(BackgroundWorker bw)
        {
            while (true)
            {
                Console.WriteLine("Waiting for broadcast");
                byte[] bytes = udpClient.Receive(ref groupEP);
                string message = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                //Console.WriteLine("Received broadcast from {0} :\n {1}\n", groupEP.ToString(), Encoding.ASCII.GetString(bytes, 0, bytes.Length));
                if(onPackageReceived != null)
                    onPackageReceived(this, new PackageReceivedEventArgs(groupEP.ToString(), message));
            }         
        }

        public event PackageReceivedHandler onPackageReceived;
    }
}
