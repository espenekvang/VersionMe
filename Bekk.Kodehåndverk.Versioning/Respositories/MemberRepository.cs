using Bekk.Kodehåndverk.Versioning.Models.V1;
using Bekk.Kodehåndverk.Versioning.Models.V2;

namespace Bekk.Kodehåndverk.Versioning.Respositories
{
    public class MemberRepository : IMemberRepository
    {
        public MemberModel Get(int memberId)
        {
            return new MemberModel {FirstName = "First"};
        }

        public MemberModelV2 GetV2(int memberId)
        {
            return new MemberModelV2 {FirstName = "First", LastName = "Last"};
        }
    }

    public interface IMemberRepository
    {
        MemberModel Get(int memberId);
        MemberModelV2 GetV2(int memberId);
    }
}