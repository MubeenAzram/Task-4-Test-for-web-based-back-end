using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BeerCollectionAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);


            //Show Available beer collection : / Beer / BeerCollection
            //Search Beer by name : / Beer / BeerCollection /{ name}
            config.Routes.MapHttpRoute(
               name: "BeerCollection",
               routeTemplate: "Beer/BeerCollection/{name}",
               defaults: new { controller = "Beer", action = "BeerCollection", name = RouteParameter.Optional }
           );

            //Add Beer in database: / Beer / CreateBeer /{ name}/{ type}/{ rating}
            config.Routes.MapHttpRoute(
                name: "CreateBeer",
                routeTemplate: "Beer/CreateBeer/{name}/{type}/{rating}",
                defaults: new { controller = "Beer", action = "CreateBeer" }
            );


            //Update Beer information in database: / Beer / UpdateBeer /{ id}/{ name}/{ type}/{ rating}
            config.Routes.MapHttpRoute(
                name: "UpdateBeer",
                routeTemplate: "Beer/UpdateBeer/{id}/{name}/{type}/{rating}",
                defaults: new { controller = "Beer", action = "UpdateBeer" }
            );

            //View Beer information: / Beer / ViewBeer /{ id}
            config.Routes.MapHttpRoute(
                name: "ViewBeer",
                routeTemplate: "Beer/ViewBeer/{id}",
                defaults: new { controller = "Beer", action = "ViewBeer"}
            );

           
        }
    }
}
