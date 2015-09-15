using System.Web.Http;
using Bekk.Kodehåndverk.Versioning.Models.V1;

namespace Bekk.Kodehåndverk.Versioning.Controllers
{
    public class MembersV2Controller : BaseApiController
    {
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(new MemberModel { FirstName = "First" });
        }
    }
}