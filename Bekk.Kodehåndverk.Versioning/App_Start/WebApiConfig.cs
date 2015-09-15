using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Bekk.Kodehåndverk.Versioning.Selectors;
using Newtonsoft.Json.Serialization;

namespace Bekk.Kodehåndverk.Versioning
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var xmlFormatter = config.Formatters.OfType<XmlMediaTypeFormatter>().FirstOrDefault();

            // Camel case names
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //Tell WebApi to create custom MediaTypes
            CreateJsonMediaTypes(jsonFormatter);
            CreateXmlMediaTypes(xmlFormatter);


            //Use your own ControllerSelector
            config.Services.Replace(typeof(IHttpControllerSelector),
              new ApiVersioningSelector(config));
        }

        private static void CreateXmlMediaTypes(MediaTypeFormatter xmlFormatter)
        {
            var mediaTypes = new List<string> {
                "application/vnd.pas.sas.v1+xml",
                "application/vnd.pas.sas.v2+xml"
            };
            CreateMediaTypes(xmlFormatter, mediaTypes);
        }

        private static void CreateJsonMediaTypes(MediaTypeFormatter jsonFormatter)
        {
            var mediaTypes = new List<string> {
                "application/vnd.pas.sas.v1+json",
                "application/vnd.pas.sas.v2+json"
            };

            CreateMediaTypes(jsonFormatter, mediaTypes);
        }

        private static void CreateMediaTypes(MediaTypeFormatter mediaTypeFormatter, IEnumerable<string> mediaTypes)
        {
            //Vendor specific media types http://www.iana.org/cgi-bin/mediatypes.pl
            foreach (var mediaType in mediaTypes)
            {
                mediaTypeFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue(mediaType));
            }
        }
    }
}
