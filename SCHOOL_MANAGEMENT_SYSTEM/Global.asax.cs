using AutoMapper;
using SCHOOL_MANAGEMENT_SYSTEM.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Net.Http;
using System.Web.Http;

namespace SCHOOL_MANAGEMENT_SYSTEM
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Mapper.Initialize(c=>c.AddProfile<MappingProfile>());
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["Language"];
            if (cookie != null && cookie.Value != null)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cookie.Value);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cookie.Value);
            }
            else
            {
                HttpCookie cookieLanguage = new HttpCookie("Language");
                cookieLanguage.Value = "km";
                Response.Cookies.Add(cookieLanguage);
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("km");
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("km");
            }

            HttpCookie cookieFullName = HttpContext.Current.Request.Cookies["Fullname"];
            if (cookieFullName == null)
            {
                HttpCookie fullName = new HttpCookie("Fullname");
                fullName.Value = "";
                Response.Cookies.Add(fullName);
            }
        }
    }
}
