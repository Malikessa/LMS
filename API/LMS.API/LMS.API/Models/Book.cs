using System;

namespace LMS.API
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Catagory { get; set; }
        public int ShelfNumber { get; set; }
        public int Price { get; set; }
        public bool IsAvailable { get; set; }
    }
}
