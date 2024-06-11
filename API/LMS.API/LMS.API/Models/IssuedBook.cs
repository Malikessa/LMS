using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.API.Models
{
    public class IssuedBook
    {
        public int LoanId { get; set; }
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

    }
}

