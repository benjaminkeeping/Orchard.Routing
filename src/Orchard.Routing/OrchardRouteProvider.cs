using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.Mvc.Routes;
using MvcRouting = Mvc.Routing.Routing;

namespace Orchard.Routing
{
    public abstract class OrchardRouteProvider : IRouteProvider
    {
        public abstract string AreaName { get; }

        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return MvcRouting.GetRoutes(GetType().Assembly).Select(route => new RouteDescriptor
            {
                Route = new Route(route.Route, new RouteValueDictionary
                {
                    {"area", AreaName}, {"controller", route.ControllerName}, {"action", route.ActionName}
                }, new RouteValueDictionary(), new RouteValueDictionary
                {
                    {"area", AreaName}
                }, new MvcRouteHandler())
            }).ToList();
        }

        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (var routeDescriptor in GetRoutes())
                routes.Add(routeDescriptor);
        }
    }
}
