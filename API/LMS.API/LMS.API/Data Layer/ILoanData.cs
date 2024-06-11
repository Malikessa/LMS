using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.API.Models;

namespace LMS.API.Data_Layer
{
    public interface ILoanData
    {
        public List<Loan> GetAllLoans();
        public Loan GetLoanById(int loanId);
        public string IssueBook(int bookId, int memberId);
        public string ReturnBook(int loanId);
    }
}
