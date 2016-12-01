using System;
using NLog;

namespace UPPY.Services.DataManagersServicesSelfHost
{
    class Program
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            var service = new DataServiceBase();
            try
            {
                service.Start(new string[] {});
                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            finally
            {
               
                service.Stop();
            }
        }
    }
}
