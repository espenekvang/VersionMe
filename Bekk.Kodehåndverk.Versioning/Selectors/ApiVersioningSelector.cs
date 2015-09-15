using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;
using Bekk.Kodehåndverk.Versioning.Resolvers;

namespace Bekk.Kodehåndverk.Versioning.Selectors
{
    public class ApiVersioningSelector : DefaultHttpControllerSelector
    {
        public ApiVersioningSelector(HttpConfiguration httpConfiguration)
            : base(httpConfiguration)
        {
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            HttpControllerDescriptor controllerDescriptor = null;

            // get list of all controllers provided by the default selector
            var controllers = GetControllerMapping();

            var routeData = request.GetRouteData();

            if (routeData == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            //check if this route is actually an attribute route
            var attributeSubRoutes = routeData.GetSubRoutes();

            var apiVersion = new VersionResolver().ResolveFrom(request);

            if (attributeSubRoutes == null)
            {
                var controllerName = GetRouteVariable<string>(routeData, "controller");
                if (controllerName == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                var newControllerName = String.Concat(controllerName, "V", apiVersion);

                if (controllers.TryGetValue(newControllerName, out controllerDescriptor))
                {
                    return controllerDescriptor;
                }
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            // we want to find all controller descriptors whose controller type names end with
            // the following suffix (example: PeopleV1)
            var newControllerNameSuffix = String.Concat("V", apiVersion);

            var filteredSubRoutes = attributeSubRoutes
                .Where(attrRouteData =>
                {
                    var currentDescriptor = GetControllerDescriptor(attrRouteData);

                    var match = currentDescriptor.ControllerName.EndsWith(newControllerNameSuffix);

                    if (match && (controllerDescriptor == null))
                    {
                        controllerDescriptor = currentDescriptor;
                    }

                    return match;
                });

            routeData.Values["MS_SubRoutes"] = filteredSubRoutes.ToArray();

            return controllerDescriptor;
        }

        private HttpControllerDescriptor GetControllerDescriptor(IHttpRouteData routeData)
        {
            return ((HttpActionDescriptor[]) routeData.Route.DataTokens["actions"]).First().ControllerDescriptor;
        }

        // Get a value from the route data, if present.
        private static T GetRouteVariable<T>(IHttpRouteData routeData, string name)
        {
            object result;
            if (routeData.Values.TryGetValue(name, out result))
            {
                return (T) result;
            }
            return default(T);
        }
    }
}