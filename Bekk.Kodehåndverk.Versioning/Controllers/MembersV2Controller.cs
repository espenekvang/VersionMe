using System.Web.Http;

namespace Bekk.Kodehåndverk.Versioning.Controllers
{
    public class MembersV2Controller : BaseApiController
    {
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(MemberRepository.GetV2(id));
        }
    }
}