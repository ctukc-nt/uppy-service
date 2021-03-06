﻿using System.Reflection;
using System.ServiceProcess;
using Mongo.Common;
using MongoDB.Driver;
using Ninject;
using Ninject.Extensions.Wcf;
using Ninject.Extensions.Wcf.SelfHost;
using Ninject.Web.Common.SelfHost;
using UPPY.DataBase.Mongo;
using UPPY.Services.Core;
using UPPY.Services.DataManagers;
using UPPY.Services.DataManagerService;

namespace UPPY.Services.DataManagersServicesSelfHost
{
    internal class DataServiceBase : ServiceBase
    {
        private NinjectSelfHostBootstrapper selfHost;

        /// <summary>
        ///     User for debugging to start the service manually.
        /// </summary>
        /// <param name="args">The arguments.</param>
        internal void Start(string[] args)
        {
            OnStart(args);
        }

        /// <summary>
        ///     When implemented in a derived class, executes when a Start command
        ///     is sent to the service by the Service Control Manager (SCM) or when
        ///     the operating system starts (for a service that starts
        ///     automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            var serviceComfiguration =
                NinjectWcfConfiguration.Create<UppyDataManagerService, NinjectServiceSelfHostFactory>();

            selfHost = new NinjectSelfHostBootstrapper(
                CreateKernel,
                serviceComfiguration);
            selfHost.Start();
        }

        /// <summary>
        ///     When implemented in a derived class, executes when a Stop command is
        ///     sent to the service by the Service Control Manager (SCM). Specifies
        ///     actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
            selfHost.Dispose();
        }

        /// <summary>
        ///     Creates the kernel.
        /// </summary>
        /// <returns>the newly created kernel.</returns>
        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            kernel.Bind(typeof(IUppyDataService)).To<UppyDataManagerService>();

            kernel.Bind(typeof(ConnectionFactory)).To(typeof(ConnectionFactory));
            kernel.Bind(typeof(MongoDbConnection)).ToMethod(x => x.Kernel.Get<ConnectionFactory>().GetConnection());
            kernel.Bind(typeof(IMongoDatabase)).ToMethod(x => x.Kernel.Get<ConnectionFactory>().GetConnection().Database);

            kernel.Bind(typeof(IObjectAuditor)).ToMethod( x=> new ObjectsAuditor(x.Kernel.Get<ConnectionFactory>().GetHistoryDbConnection().Database));
            kernel.Bind(typeof(IGetterHistoryRecords)).ToMethod(x => new ObjectsAuditor(x.Kernel.Get<ConnectionFactory>().GetHistoryDbConnection().Database));

            kernel.Bind(typeof(CollectionsContainer)).To<CollectionsContainer>();
            kernel.Bind(typeof(EntityCommonDataManagers)).ToMethod(x => new EntityCommonDataManagers() { CollectionsContainer = x.Kernel.Get<CollectionsContainer>(), Auditor = x.Kernel.Get<IObjectAuditor>() });
            kernel.Bind(typeof(HistoryEntityManager)).ToMethod(x => new HistoryEntityManager() { Auditor = x.Kernel.Get<IGetterHistoryRecords>() });

            return kernel;
        }
    }
}