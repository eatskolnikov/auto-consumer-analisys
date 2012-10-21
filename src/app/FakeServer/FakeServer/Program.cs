using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FakeServer
{
    class Program
    {
        static void Main(string[] args)
        {
            UDPListener.StartListener();
        }
    }
}
