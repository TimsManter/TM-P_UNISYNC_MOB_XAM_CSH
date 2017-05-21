using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace UniSyncServerConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(UniSyncService.UniSyncService)))
            {
                host.Open();
                Console.ReadKey();
            }
        }
    }
}
