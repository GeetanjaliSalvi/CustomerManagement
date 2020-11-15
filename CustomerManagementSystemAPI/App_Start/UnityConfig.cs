using CustomerManagementDAL.Repositories;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace CustomerManagementSystemAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<ICustomerRepository, CustomerRepository>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}