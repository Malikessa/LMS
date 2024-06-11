using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using LMS.API.Models;

namespace LMS.API.Data_Layer
{
    public class LoanData : ILoanData
    {
        private readonly string _connectionString;
        //making connection to database
        public LoanData(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("LMSConnectionString");
        }

        public List<Loan> GetAllLoans()
        {
            List<Loan> loans = new List<Loan>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Loans";

                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Loan loan = new Loan
                        {
                            LoanId = Convert.ToInt32(reader["LoanId"]),
                            BookId = Convert.ToInt32(reader["BookId"]),
                            MemberId = Convert.ToInt32(reader["MemberId"]),
                            IssueDate = (DateTime)reader["LoanDate"],
                            ReturnDate = (reader["ReturnDate"] == DBNull.Value) ? null : (DateTime?)reader["ReturnDate"],
                            // Add other properties as needed
                        };

                        loans.Add(loan);
                    }
                }
            }
            return loans;
        }

        public Loan GetLoanById(int loanId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetLoanById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LoanId", loanId);
                
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Loan loan=new Loan
                        {
                            LoanId = Convert.ToInt32(reader["LoanId"]),
                            BookId = Convert.ToInt32(reader["BookId"]),
                            MemberId = Convert.ToInt32(reader["MemberId"]),
                            IssueDate = Convert.ToDateTime(reader["LoanDate"]),
                            ReturnDate = (reader["ReturnDate"] == DBNull.Value) ? null : (DateTime?)reader["ReturnDate"],

                            // Add other properties as needed
                        };

                        return loan;
                    }
                }
                return null;

            }
        }

        public string IssueBook(int bookId, int memberId)
        {
            string message = "";
            using (SqlConnection connection=new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("IssueBook", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@BookId", bookId);
                command.Parameters.AddWithValue("@MemberId", memberId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        message = reader["Message"].ToString();
                    }
                }
            }

            return message;
        }

        public string ReturnBook(int loanId)
        {
            string message = "";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("ReturnBook", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LoanId", loanId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        message = reader["Message"].ToString();
                    }
                }
            }

            return message;
        }
    }
}
