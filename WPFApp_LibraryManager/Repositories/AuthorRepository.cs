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

            DataTable authorTable = GetResultTable(SqlQueries.GetAuthorListQuery);

            foreach (DataRow authorRow in authorTable.Rows)
            {
                Author author = new Author();
                author.Id = (int)authorRow["AuthorId"];
                author.FirstName = (string)authorRow["FirstName"];
                author.LastName = (string)authorRow["LastName"];
                author.FullName = (string)authorRow["AuthorFullName"];

                if (DBNull.Value.Equals(authorRow["MiddleName"]))
                {
                    author.MiddleName = "";
                }
                else
                {
                    author.MiddleName = (string)authorRow["MiddleName"];
                }

                authorList.Add(author);
            }

            return authorList;
        }

        public List<Author> GetFilteredAuthorList(string searchString)
        {
            SqlCommand cmd = new SqlCommand(SqlQueries.GetFilteredAuthorListQuery, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@SearchString", searchString);

            DataTable authorTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            using (sqlDataAdapter)
            {
                sqlDataAdapter.Fill(authorTable);
            }

            List<Author> authorList = new List<Author>();

            foreach (DataRow authorRow in authorTable.Rows)
            {
                Author author = new Author();
                author.Id = (int)authorRow["AuthorId"];
                author.FirstName = (string)authorRow["FirstName"];
                author.LastName = (string)authorRow["LastName"];
                author.FullName = (string)authorRow["AuthorFullName"];

                if (DBNull.Value.Equals(authorRow["MiddleName"]))
                {
                    author.MiddleName = "";
                }
                else
                {
                    author.MiddleName = (string)authorRow["MiddleName"];
                }

                authorList.Add(author);
            }

            return authorList;
        }

        public void InsertAuthor(Author author)
        {
            try
            {
                _sqlConnection.Open();

                SqlCommand cmd = new SqlCommand(SqlQueries.InsertAuthorQuery, _sqlConnection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@AuthorFirstName", author.FirstName);
                cmd.Parameters.AddWithValue("@AuthorMiddleName", author.MiddleName);
                cmd.Parameters.AddWithValue("@AuthorLastName", author.LastName);

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public void UpdateAuthor(Author author)
        {
            try
            {
                _sqlConnection.Open();

                SqlCommand cmd = new SqlCommand(SqlQueries.UpdateAuthorQuery, _sqlConnection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@AuthorId", author.Id);
                cmd.Parameters.AddWithValue("@AuthorFirstName", author.FirstName);
                cmd.Parameters.AddWithValue("@AuthorMiddleName", author.MiddleName);
                cmd.Parameters.AddWithValue("@AuthorLastName", author.LastName);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public void DeleteAuthor(int authorId)
        {
            try
            {
                _sqlConnection.Open();

                SqlCommand cmd = new SqlCommand(SqlQueries.DeleteAuthorQuery, _sqlConnection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@AuthorId", authorId);

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }
    }
}
