using System.Web.Http;

namespace Bekk.Kodehåndverk.Versioning.Controllers
{
    public class MembersV2Controller : BaseApiController
    {
        public IHttpActionResult Get()
        {
            return Ok(new { Name = "A version 2 member" });
        }
    }
}