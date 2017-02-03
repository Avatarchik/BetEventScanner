using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BetEventScanner.Common.DataModel;
using BetEventScanner.Common.Services;

namespace BetEventScanner.Web.Client
{
    public class WebApiApplication : HttpApplication
    {
        private static OddsService _oddsService;

        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            _oddsService = new OddsService(new List<Division> { Division.EnglandApl });
        }
    }
}
