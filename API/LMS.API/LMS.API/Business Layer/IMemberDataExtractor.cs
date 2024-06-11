using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.API.Business_Layer
{
    public interface IMemberDataExtractor
    {
        public List<Member> GetAllMembers();
        public Member GetMemberById(int memberId);
        public string AddMember(Member member);
        public string UpdateMember(int memberId, Member member);
        public string DeleteMember(int memberId);
    }
}
