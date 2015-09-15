using System.Web.Http;

namespace Bekk.Kodehåndverk.Versioning.Controllers
{
    public class MembersV1Controller : BaseApiController
    {
        public IHttpActionResult Get()
        {
            return Ok(new { Name= "A member"});
        }
    }
}