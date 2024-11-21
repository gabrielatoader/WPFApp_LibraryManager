using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;
using WPFApp_LibraryManager.Utils;

namespace WPFApp_LibraryManager.Repositories
{
    public class AuthorRepository : BaseRepository, IAuthorRepository
    {
        public List<Author> GetAuthorList()
        {
            List<Author> authorList = new List<Author>();

            DataTable authorsTable = GetResultTable(SqlQueries.GetAuthorListQuery);

            foreach (DataRow authorRow in authorsTable.Rows)
            {
                Author author = new Author();
                author.Id = (int)authorRow["AuthorId"];
                author.FirstName = (string)authorRow["FirstName"];
                if (DBNull.Value.Equals(authorRow["MiddleName"]))
                {
                    author.MiddleName = "";
                }
                else 
                {
                    author.MiddleName = (string)authorRow["MiddleName"];
                }
                author.LastName = (string)authorRow["LastName"];
                author.FullName = (string)authorRow["AuthorFullName"];

                authorList.Add(author);
            }

            return authorList;
        }

        public List<Author> GetFilteredAuthorList(string searchString)
        {
            SqlCommand cmd = new SqlCommand(SqlQueries.GetFilteredAuthorListQuery, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@SearchString", searchString);

            DataTable authorsTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            using (sqlDataAdapter)
            {
                sqlDataAdapter.Fill(authorsTable);
            }

            List<Author> authorList = new List<Author>();

            foreach (DataRow authorRow in authorsTable.Rows)
            {
                Author author = new Author();
                author.Id = (int)authorRow["AuthorId"];
                author.FirstName = (string)authorRow["FirstName"];
                if (DBNull.Value.Equals(authorRow["MiddleName"]))
                {
                    author.MiddleName = "";
                }
                else
                {
                    author.MiddleName = (string)authorRow["MiddleName"];
                }
                author.LastName = (string)authorRow["LastName"];
                author.FullName = (string)authorRow["AuthorFullName"];

                authorList.Add(author);
            }

            return authorList;
        }

        public void InsertAuthor(Author author)
        {
            _sqlConnection.Open();

            SqlCommand cmd = new SqlCommand(SqlQueries.InsertAuthorQuery, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@AuthorFirstName", author.FirstName);
            cmd.Parameters.AddWithValue("@AuthorMiddleName", author.MiddleName);
            cmd.Parameters.AddWithValue("@AuthorLastName", author.LastName);
            
            cmd.ExecuteNonQuery();

            _sqlConnection.Close();
        }

        public void UpdateAuthor(Author author)
        {
            _sqlConnection.Open();

            SqlCommand cmd = new SqlCommand(SqlQueries.UpdateAuthorQuery, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@AuthorId", author.Id);
            cmd.Parameters.AddWithValue("@AuthorFirstName", author.FirstName);
            cmd.Parameters.AddWithValue("@AuthorMiddleName", author.MiddleName);
            cmd.Parameters.AddWithValue("@AuthorLastName", author.LastName);
            cmd.ExecuteNonQuery();

            _sqlConnection.Close();
        }

        public void DeleteAuthor(int authorId)
        {
            _sqlConnection.Open();

            SqlCommand cmd = new SqlCommand(SqlQueries.DeleteAuthorQuery, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@AuthorId", authorId);

            cmd.ExecuteNonQuery();

            _sqlConnection.Close();
        }
    }
}
