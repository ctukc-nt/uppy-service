using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using NLog;

namespace UPPY.Services.DataManagerService
{
    internal class LoggingErrorHandler : IErrorHandler
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            _logger.Error($"Exception: {0}. MessageVersion: {1}. Message: {2}.", error, version, fault);
        }

        public bool HandleError(Exception error)
        {
            _logger.Error(error);
            return false; // здесь можно ставить бряки, логировать и т.п.
        }
    }
}