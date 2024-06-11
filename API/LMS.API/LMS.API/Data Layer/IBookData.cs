using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.API.Data_Layer
{
    public interface IBookData
    {
        public List<Book> GetAllBooks();
        public Book GetBookById(int bookId);
        public string AddBook(Book book);
        public string UpdateBook(int bookId,Book book);
        public string DeleteBook(int bookId);
      
    }
}
