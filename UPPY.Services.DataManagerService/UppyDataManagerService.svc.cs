using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Core.Interfaces;
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

        private readonly EntityCommonDataManagers _dataManagers;
        private readonly HistoryEntityManager _historyManager;

        public UppyDataManagerService(EntityCommonDataManagers dataManagers, HistoryEntityManager historyManager)
        {
            _dataManagers = dataManagers;
            _historyManager = historyManager;
        }

        private List<T> GetAllChildrensCashed<T>(int? parentId, List<T> cashed) where T:IHierarchyEntity
        {
            var result = new List<T>();
            var childrens = GetChildrenDrawingsCashed(parentId, cashed);
            result.AddRange(childrens);
            foreach (var child in childrens)
            {
                result.AddRange(GetAllChildrensCashed(child.Id, cashed));
            }

            return result;
        }

        private List<T> GetChildrenDrawingsCashed<T>(int? parentId, List<T> cashed) where T : IHierarchyEntity
        {
            return cashed.Where(x => x.ParentId == parentId).ToList();
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