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
    public class MemberData : IMemberData
    {
        private readonly string _connectionString ;
        //making connection to database
        public MemberData(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("LMSConnectionString");
        }
        public List<Member> GetAllMembers()
        {
            List<Member> members = new List<Member>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Members";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, connection);
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);

                foreach(DataRow dataRow in dtbl.Rows)
                {
                    Member member = new Member
                    {
                        MemberId = Convert.ToInt32(dataRow["MemberId"]),
                        FirstName = dataRow["FirstName"].ToString(),
                        LastName = dataRow["LastName"].ToString(),
                        Email = dataRow["Email"].ToString(),
                        // Add other properties as needed
                    };
                    member.bookIssuedList = GetIssuedBooksForMember(member.MemberId);

                    members.Add(member);
                }   
            }

            return members;
        }
        public Member GetMemberById(int memberId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetMemberData", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MemberId", memberId);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = command;

                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    Member member = new Member
                    {
                        MemberId = Convert.ToInt32(dataRow["MemberId"]),
                        FirstName = dataRow["FirstName"].ToString(),
                        LastName = dataRow["LastName"].ToString(),
                        Email = dataRow["Email"].ToString(),
                        // Add other properties as needed
                    };

                    member.bookIssuedList = GetIssuedBooksForMember(memberId);
                    return member;
                }
            }

            return null;
        }
        private List<IssuedBook> GetIssuedBooksForMember(int memberId)
        {
            List<IssuedBook> issuedBooks = new List<IssuedBook>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("GetIssuedBooksByMemberId", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MemberId", memberId);

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = command;

                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach(DataRow dataRow in dataTable.Rows)
                {
                    IssuedBook issuedBook = new IssuedBook
                    {
                        LoanId = Convert.ToInt32(dataRow["LoanId"]),
                        BookId = Convert.ToInt32(dataRow["BookId"]),
                        BookTitle = dataRow["BookTitle"].ToString(),
                        IssueDate = (DateTime)dataRow["LoanDate"],
                        ReturnDate = (dataRow["ReturnDate"] == DBNull.Value) ? null : (DateTime?)dataRow["ReturnDate"]
                    };

                    issuedBooks.Add(issuedBook);
                }
            }
            return issuedBooks;
        }
        public string AddMember(Member member)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("AddMember", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FirstName", member.FirstName);
                command.Parameters.AddWithValue("@LastName", member.LastName);
                command.Parameters.AddWithValue("@Email", member.Email);

                command.ExecuteNonQuery();
            }
            return "Member has been added successfully!";
        }
        public string DeleteMember(int memberId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("DeleteMember", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MemberId", memberId);

                command.ExecuteNonQuery();
            }
            return "Member has been deleted successfully!";
        }
        public string UpdateMember(int memberId, Member member)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("UpdateMember", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MemberId", memberId);
                command.Parameters.AddWithValue("@FirstName", member.FirstName);
                command.Parameters.AddWithValue("@LastName", member.LastName);
                command.Parameters.AddWithValue("@Email", member.Email);

                command.ExecuteNonQuery();
            }
            return "Member has been updated successfully!";
        }
    }  
}
