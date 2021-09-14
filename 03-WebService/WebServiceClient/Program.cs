using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WebServiceClient.WebServiceServer;

namespace WebServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var webServiceDemo = new WebServiceServer.WebServiceDemo();
            webServiceDemo.CertificateSoapHeaderValue = new CertificateSoapHeader()
            {
                UserName = "111",
                Password = "222"
            };

            var ret = webServiceDemo.HelloWorld();
            var ret1 = webServiceDemo.Sum(11, 22);


            Console.ReadKey();
        }
    }
}
