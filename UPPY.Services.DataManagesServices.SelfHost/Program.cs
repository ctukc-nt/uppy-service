using System;

namespace UPPY.Services.DataManagersServicesSelfHost
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
