using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.API.Business_Layer;
using LMS.API.Models;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : Controller
    {
        private readonly ILoanDataExtractor _loanDataExtractor;
        public LoansController(ILoanDataExtractor loanDataExtractor)
        {
            _loanDataExtractor = loanDataExtractor;
        }

        [HttpGet("GetAllLoans")]
        public ActionResult<List<Loan>> GetAllLoans(int bookId, int memberId)
        {
            try
            {
                return _loanDataExtractor.GetAllLoans();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500,"Internal Server Error");
            }
        }

        [HttpGet("GetLoanById")]
        public ActionResult<Loan> GetLoanById(int loanId)
        {
            try
            {
                Loan loan= _loanDataExtractor.GetLoanById(loanId);
                if (loan == null)
                    return NotFound(); // 404 Not Found

                return Ok(loan);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("IssueBook")]
        public IActionResult IssueBook(Loan loan)
        {
            try
            {
                return new JsonResult(_loanDataExtractor.IssueBook(loan.BookId, loan.MemberId));
            }
            catch (Exception ex)
            {
                // Log the exception
                return new JsonResult("Internal Server Error");
            }
        }

        [HttpPost("ReturnBook")]
        public IActionResult ReturnBook(int loanId)
        {
            try
            {
                return new JsonResult(_loanDataExtractor.ReturnBook(loanId));
            }
            catch (Exception ex)
            {
                // Log the exception
                return new JsonResult("Internal Server Error");
            }
        }
    }
}
