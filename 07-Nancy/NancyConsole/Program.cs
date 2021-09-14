using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Hosting.Self;

namespace NancyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            NancyHost _Host = null;
            HostConfiguration hostConfigs = new HostConfiguration()
            {
                UrlReservations = new UrlReservations() { CreateAutomatically = true }
            };

            _Host = new NancyHost(hostConfigs, new Uri("http://localhost:8000"));
            _Host.Start();
            Console.ReadLine();

        }
    }
}
