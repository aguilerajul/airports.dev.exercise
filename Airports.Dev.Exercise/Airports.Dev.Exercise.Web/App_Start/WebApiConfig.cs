using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Airports.Dev.Exercise.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "AirportDistanceApi",
                routeTemplate: "api/{controller}/{iataCode1}/{iataCode2}",
                defaults: new { iataCode1 = RouteParameter.Optional, iataCode2 = RouteParameter.Optional }
            );
        }
    }
}
