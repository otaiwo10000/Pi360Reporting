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

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
//[assembly: OwinStartup(typeof(AccessBankAccountRegularizationWebAPI.Startup))]  //ensure you add this line/assembly. It is important.
namespace Pi360Reporting
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static readonly ILog log = LogManager.GetLogger("File1Appender");
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }

        protected void Application_PostAuthorizeRequest()
        {
            HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }

        ////the below should be added on "Page_Load" or here 
        //protected void Application_BeginRequest()
        //{
        //    HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    HttpContext.Current.Response.Cache.SetNoServerCaching();
        //    HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.AddHours(-1));
        //    HttpContext.Current.Response.Cache.SetNoStore();
        //}

       
        void Application_Error(object sender, EventArgs e)
        {
            string customerrormsg = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["CustomErrorMessage"]);

            Exception ex = Server.GetLastError();
            log.ErrorFormat("{0}{1}", "Application_Error: ", ex);        // "....... \n"  +  ex;

            //Response.Clear();
            ////Response.Write("Something not yet");
            //Response.Write(customerrormsg);
            //Server.ClearError();
        }

    }
}
