using BankDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BankDashboard
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }


        void Application_PreSendRequestHeaders(Object sender, EventArgs e)
        {
            if (HttpContext.Current != null)
            {
                Response.Headers.Set("Cache-Control", "no-cache");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
                Response.Cache.SetNoStore();

                Response.Headers.Set("content-Security-Policy", "true");
                Response.Headers.Set("X-XSS-Protection", "1; mode=block");
                Response.Headers.Set("strict-Transport-Security", "1; max-age=31536000");
                Response.Headers.Remove("X-AspNetMvc-Version");
                Response.Headers.Remove("Server");
                Response.Headers.Remove("X-AspNet-Version");
                Response.Headers.Remove("X-SourceFiles");
            }

        }
       
        void Session_End(object sender, EventArgs e)
        {
            Session.Abandon();

        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current != null)
            {
                Request.Headers.Remove("Referer");
                Request.Headers.Remove("Cookie");
                Request.Headers.Remove("Host");
            }
        }

    }
}