using System.Net;
using System.Threading;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace webapp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            KeepAliveThread.Start();
        }

        static readonly Thread KeepAliveThread = new Thread(KeepAlive);

        protected void Application_End()
        {
            KeepAliveThread.Abort();
        }

        static void KeepAlive()
        {
            while (true)
            {
                var req = WebRequest.Create("http://aia.apphb.com");
                req.GetResponse();
                try
                {
                    Thread.Sleep(5*60000);
                }
                catch (ThreadAbortException)
                {
                    break;
                }
            }
        }
    }
}
