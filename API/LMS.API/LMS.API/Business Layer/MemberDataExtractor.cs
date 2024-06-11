using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.API.Data_Layer;

namespace LMS.API.Business_Layer
{
    public class MemberDataExtractor : IMemberDataExtractor
    {
        IMemberData _memberData;
        public MemberDataExtractor(IMemberData memberData)
        {
            _memberData = memberData;
        }
        public string AddMember(Member member)
        {
            return _memberData.AddMember(member);
        }

        public string DeleteMember(int memberId)
        {
            return _memberData.DeleteMember(memberId);
        }

        public List<Member> GetAllMembers()
        {
            return _memberData.GetAllMembers();
        }

        public Member GetMemberById(int memberId)
        {
            return _memberData.GetMemberById(memberId);
        }

        public string UpdateMember(int memberId, Member member)
        {
            return _memberData.UpdateMember(memberId, member);
        }
    }
}
