using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using NLog;
using UPPY.Services.DataManagers;

namespace UPPY.Services.DataManagerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [LoggingServiceBehavior]
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

    [AttributeUsage(AttributeTargets.Class)]
    public class LoggingServiceBehaviorAttribute : Attribute, IServiceBehavior
    {
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        public void AddBindingParameters(
            ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
            {
                channelDispatcher.ErrorHandlers.Add(new LoggingErrorHandler());
            }
        }
    }
}