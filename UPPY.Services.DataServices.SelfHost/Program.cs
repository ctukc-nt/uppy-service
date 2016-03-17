using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPPY.Services.DataServicesSelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new DataServiceBase();
            try
            {
                service.Start(new string[] { });
                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }
            finally
            {
                service.Stop();
            }
        }
    }
}
