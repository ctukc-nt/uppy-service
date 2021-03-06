﻿using System.Reflection;
using System.ServiceProcess;
using Core.Domain.Interfaces.SpecialsDataManagers;
using Core.Interfaces;
using Core.Security;
using Mongo.Common;
using Ninject;
using Ninject.Extensions.Wcf;
using Ninject.Extensions.Wcf.SelfHost;
using Ninject.Web.Common.SelfHost;
using UPPY.DataBase.Mongo;
using UPPY.ServerService;

namespace UPPY.Services.DataServicesSelfHost
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
                NinjectWcfConfiguration.Create<UppyService, NinjectServiceSelfHostFactory>();

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

            kernel.Bind(typeof(ConnectionFactory)).To(typeof(ConnectionFactory));
            kernel.Bind(typeof(MongoDbConnection)).ToMethod(x => x.Kernel.Get<ConnectionFactory>().GetConnection());

            kernel.Bind(typeof(IMongoDataBaseWithUser)).To(typeof(Database));
            kernel.Bind(typeof(IUppyDataManagersFactory)).To(typeof(UppyDataMangersFactory));
            kernel.Bind(typeof(IDataManagersFactory)).To(typeof(UppyDataMangersFactory));

            kernel.Bind(typeof(ITicketAutUser)).ToMethod(x => new TicketAutUser("web", "bad ticket"));

            return kernel;
        }
    }
}