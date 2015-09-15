using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace Bekk.Kodehåndverk.Versioning.Resolvers
{
    public class VersionResolver
    {
        private const int LatestApiVersion = 2;

        public int ResolveFrom(HttpRequestMessage request)
        {
            var acceptHeader = request.Headers.Accept;

            int version;
            if (int.TryParse(GetVersionFromMediaType(acceptHeader, FileFormat.Json), out version))
            {
                return ValidateAndReturn(version);
            }

            if (int.TryParse(GetVersionFromMediaType(acceptHeader, FileFormat.Xml), out version))
            {
                return ValidateAndReturn(version);
            }

            return LatestApiVersion;
        }

        private static int ValidateAndReturn(int version)
        {
            if (version > LatestApiVersion)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return version;
        }


        private static string GetVersionFromMediaType(IEnumerable<MediaTypeWithQualityHeaderValue> acceptHeader, string fileFormat)
        {
            var regularExpression = new Regex(@"application\/vnd\.bekk\.([a-z]+)\.v([0-9]+)\+" + fileFormat,
                RegexOptions.IgnoreCase);

            foreach (var match in acceptHeader.Select(mime => regularExpression.Match(mime.MediaType)).Where(match => match.Success))
            {
                return match.Groups[2].Value;
            }

            return string.Empty;
        }

        internal class FileFormat
        {
            public const string Json = "json";
            public const string Xml = "xml";
        }
    }
}