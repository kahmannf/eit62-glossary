using GlossaryDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GlossaryBackend
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlossaryInMemoryBusiness.Config budinessConfig = GlossaryInMemoryBusiness.Config.GetInstance();
            budinessConfig.DataDirectory = "c:\\temp";

            GlossaryInMemoryBusiness.ExceptionHandler.ExceptionThrown += ExceptionHandler_ExceptionThrown;
        }

        private void ExceptionHandler_ExceptionThrown(Exception ex)
        {

        }

        public static IEntryBusiness GetEntryBusiness() => GlossaryInMemoryBusiness.API.GetEntryBusiness();
        public static IUserBusiness GetUserBusiness() => GlossaryInMemoryBusiness.API.GetUserBusiness();
    }
}
