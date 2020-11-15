using CustomerManagementDAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace CustomerManagementSystemAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Setup unit dependency resolver
            UnityConfig.RegisterComponents();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.MessageHandlers.Add(new ApplicationAuthenticationHandler());
            using (var context = new CustomerContext())
            {
                context.Database.Initialize(force: false);
            }
        }
    }
}
