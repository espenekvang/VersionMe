using System.Web.Http;
using Bekk.Kodehåndverk.Versioning.Models.V1;

namespace Bekk.Kodehåndverk.Versioning.Controllers
{
    public class MembersV1Controller : BaseApiController
    {
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(MemberRepository.Get(id));
        }
    }
}