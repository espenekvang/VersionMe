using System.Web.Http;

namespace Bekk.Kodehåndverk.Versioning.Controllers
{
    public class MembersController : BaseApiController
    {
        public IHttpActionResult Get()
        {
            return Ok(new { Name= "A member"});
        }
    }
}