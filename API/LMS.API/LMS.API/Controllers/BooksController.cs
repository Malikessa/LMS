using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using LMS.API.Business_Layer;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookDataExtractor _bookDataExtractor;

        public BooksController(IBookDataExtractor bookDataExtractor)
        {
            _bookDataExtractor= bookDataExtractor;
        }

        [HttpGet("GetBooks")]
        public ActionResult<List<Book>> GetAllBooks()
        {
            try
            {
                List<Book> books = _bookDataExtractor.GetAllBooks();
                return Ok(books);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("GetBookById")]
        public ActionResult<Book> GetBookById(int bookId)
        {
            try
            {
                Book book = _bookDataExtractor.GetBookById(bookId);

                if (book == null)
                    return NotFound(); // 404 Not Found

                return Ok(book);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("AddBook")]
        public IActionResult AddBook(Book book)
        {
            try
            {
                return new JsonResult(_bookDataExtractor.AddBook(book));
            }
            catch (Exception ex)
            {
                // Log the exception
                return new JsonResult("Internal Server Error");
            }
        }
        [HttpPost("UpdateBook")]
        public IActionResult UpdateBook(Book book)
        {
            try
            {
                return new JsonResult(_bookDataExtractor.UpdateBook(book.BookId, book)); ;
            }
            catch (Exception ex)
            {
                // Log the exception
                return new JsonResult("Internal Server Error");
            }
        }
        [HttpDelete("DeleteBook")]
        public IActionResult DeleteBook(int bookId)
        {
            try
            {
                return new JsonResult(_bookDataExtractor.DeleteBook(bookId));
            }
            catch (Exception ex)
            {
                // Log the exception
                return new JsonResult("Internal Server Error");
            }
        }
        // Add other methods for update and delete as needed
    }
}
