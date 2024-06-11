using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.API.Data_Layer;


namespace LMS.API.Business_Layer
{
    public class BookDataExtractor : IBookDataExtractor
    {
        private readonly IBookData _bookData;
        public BookDataExtractor(IBookData bookData)
        {
            _bookData = bookData;
        }
        public string AddBook(Book book)
        {
            return _bookData.AddBook(book);
        }

        public string DeleteBook(int bookId)
        {
            return _bookData.DeleteBook(bookId);
        }

        public List<Book> GetAllBooks()
        {
            return _bookData.GetAllBooks();
        }

        public Book GetBookById(int bookId)
        {
            return _bookData.GetBookById(bookId);
        }

        public string UpdateBook(int bookId, Book book)
        {
            return _bookData.UpdateBook(bookId, book);
        }
    }
}
