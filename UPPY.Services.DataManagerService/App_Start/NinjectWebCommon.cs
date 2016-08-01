using System;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Mongo.Common;
using MongoDB.Driver;
using Ninject;
using Ninject.Web.Common;
using UPPY.DataBase.Mongo;
using UPPY.Services.Core;
using UPPY.Services.DataManagers;
using UPPY.Services.DataManagerService;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(NinjectWebCommon), "Stop")]

namespace UPPY.Services.DataManagerService
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(typeof(IUppyDataService)).To<UppyDataManagerService>();

            kernel.Bind(typeof(ConnectionFactory)).To(typeof(ConnectionFactory));
            kernel.Bind(typeof(MongoDbConnection)).ToMethod(x => x.Kernel.Get<ConnectionFactory>().GetConnection());
            kernel.Bind(typeof(IMongoDatabase)).ToMethod(x => x.Kernel.Get<ConnectionFactory>().GetConnection().Database);

            kernel.Bind(typeof(CollectionsContainer)).To<CollectionsContainer>();
            kernel.Bind(typeof(CommonEntityDataManagers)).ToMethod(x => new CommonEntityDataManagers() { CollectionsContainer = x.Kernel.Get<CollectionsContainer>() });


        }
    }
}
