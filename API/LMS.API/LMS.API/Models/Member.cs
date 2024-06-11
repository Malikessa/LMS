using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.API.Models;

namespace LMS.API
{
    public class Member
    {
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<IssuedBook> bookIssuedList { get; set; }
    }
}
