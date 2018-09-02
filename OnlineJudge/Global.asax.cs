using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using log4net;

namespace OnlineJudge {
    public class MvcApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            //Configures the log4net configurator to launch with application.
            log4net.Config.XmlConfigurator.Configure();
            //We will use a Debug message when the application starts.
            var log = LogManager.GetLogger("logger");
            log.Debug("Application Started");
        }

        protected void Application_PostAuthorizeRequest()
        {
            if(IsWebApiRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }

        private bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.
                StartsWith(WebApiConfig.UrlPrefixRelative);
        }
    }
}
