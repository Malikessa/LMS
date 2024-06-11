using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.API.Data_Layer;
using LMS.API.Models;

namespace LMS.API.Business_Layer
{
    public class LoanDataExtractor : ILoanDataExtractor
    {
        private readonly ILoanData _loanData;
        public LoanDataExtractor(ILoanData loanData)
        {
            _loanData = loanData;
        }

        public List<Loan> GetAllLoans()
        {
            return _loanData.GetAllLoans();
        }

        public Loan GetLoanById(int loanId)
        {
            return _loanData.GetLoanById(loanId);
        }

        public string IssueBook(int bookId, int memberId)
        {
            return _loanData.IssueBook(bookId, memberId);
        }

        public string ReturnBook(int loanId)
        {
            return _loanData.ReturnBook(loanId);
        }
    }
}
