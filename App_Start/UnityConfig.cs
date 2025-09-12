using PruebaTecnica.Controllers;
using PruebaTecnica.Models;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace PruebaTecnica
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // Registra DbContext y repositorio
            container.RegisterType<MiDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductoRepository, ProductoRepository>();
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}