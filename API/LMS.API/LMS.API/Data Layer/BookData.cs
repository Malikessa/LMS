using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Extensions.Configuration;


namespace LMS.API.Data_Layer
{
    public class BookData : IBookData
    {
        string connectionString = "";
        //making connection with database
        public BookData(IConfiguration config)
        {
            var c = config;
            connectionString = config.GetConnectionString("LMSConnectionString");
        }
        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Books";
                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book book = new Book
                        {
                            BookId = Convert.ToInt32(reader["BookId"]),
                            Title = reader["Title"].ToString(),
                            Catagory = reader["Catagory"].ToString(),
                            ShelfNumber = Convert.ToInt32(reader["ShelfNumber"]),
                            Price = Convert.ToInt32(reader["Price"]),
                            IsAvailable = Convert.ToBoolean(reader["isAvailable"]),
                            // Add other properties as needed
                        };

                        books.Add(book);
                    }
                }
            }

            return books;
        }

        public Book GetBookById(int bookId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("GetBookData", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@BookId", bookId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Book book = new Book
                        {
                            BookId = Convert.ToInt32(reader["BookId"]),
                            Title = reader["Title"].ToString(),
                            Catagory = reader["Catagory"].ToString(),
                            ShelfNumber = Convert.ToInt32(reader["ShelfNumber"]),
                            Price = Convert.ToInt32(reader["Price"]),
                            IsAvailable = Convert.ToBoolean(reader["isAvailable"])
                            // Add other properties as needed
                        };

                        return book;
                    }
                }
            }
            return null;
        }
        public string AddBook(Book book)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("AddBook", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Catagory", book.Catagory);
                command.Parameters.AddWithValue("@ShelfNumber", book.ShelfNumber);
                command.Parameters.AddWithValue("@Price", book.Price);
                command.Parameters.AddWithValue("@IsAvailable", (book.IsAvailable==true) ? 1 : 0);

                command.ExecuteNonQuery();
            }
            return "Book has been added successfully!";
        }

        public string UpdateBook(int bookId, Book book)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("UpdateBook", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@BookId", bookId);
                command.Parameters.AddWithValue("@Title", book.Title);
                command.Parameters.AddWithValue("@Catagory", book.Catagory);
                command.Parameters.AddWithValue("@ShelfNumber", book.ShelfNumber);
                command.Parameters.AddWithValue("@Price", book.Price);
                command.Parameters.AddWithValue("@IsAvailable", (book.IsAvailable == true) ? 1 : 0);

                command.ExecuteNonQuery();
            }
            return "Book has been updated successfully!";
        }

        public string DeleteBook(int bookId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("DeleteBook", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@BookId", bookId);

                command.ExecuteNonQuery();
            }
            return "Book has been deleted successfully!";
        }
    }
}
