using SCHOOL_MANAGEMENT_SYSTEM.App_Start;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SCHOOL_MANAGEMENT_SYSTEM
{
    public static class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {
            var settings = config.Formatters.JsonFormatter.SerializerSettings;

            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            settings.Formatting = Formatting.Indented;

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //var json = config.Formatters.JsonFormatter;
            //json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            //config.Formatters.Remove(config.Formatters.XmlFormatter);
        }

    }
}
