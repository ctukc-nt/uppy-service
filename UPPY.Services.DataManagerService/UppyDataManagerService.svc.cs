using NLog;
using UPPY.Services.DataManagers;

namespace UPPY.Services.DataManagerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public partial class UppyDataManagerService
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private readonly CommonEntityDataManagers _dataManagers;

        public UppyDataManagerService(CommonEntityDataManagers dataManagers)
        {
            _dataManagers = dataManagers;
            _logger.Trace("Created instance");
        }
    }
}