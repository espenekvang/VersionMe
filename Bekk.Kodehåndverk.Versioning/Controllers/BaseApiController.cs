using System.Web.Http;
using Bekk.Kodehåndverk.Versioning.Respositories;

namespace Bekk.Kodehåndverk.Versioning.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        protected MemberRepository MemberRepository { get; set; }

        protected BaseApiController()
        {
            MemberRepository = new MemberRepository();
        }
    }
}