using System.Web.Http;
using Bekk.Kodehåndverk.Versioning.Models.V2;

namespace Bekk.Kodehåndverk.Versioning.Controllers
{
    public class MembersV2Controller : BaseApiController
    {
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(new MemberModelV2 { FirstName = "First", LastName = "Last"});
        }
    }
}