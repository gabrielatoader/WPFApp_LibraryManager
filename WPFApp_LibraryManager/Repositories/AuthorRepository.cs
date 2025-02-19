using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
                cmd.Parameters.AddWithValue("@AuthorFirstName", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(author.FirstName.Trim()));
                cmd.Parameters.AddWithValue("@AuthorMiddleName", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(author.MiddleName.Trim()));
                cmd.Parameters.AddWithValue("@AuthorLastName", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(author.LastName.Trim()));

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
                cmd.Parameters.AddWithValue("@AuthorFirstName", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(author.FirstName.Trim()));
                cmd.Parameters.AddWithValue("@AuthorMiddleName", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(author.MiddleName.Trim()));
                cmd.Parameters.AddWithValue("@AuthorLastName", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(author.LastName.Trim()));
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

        public bool IsAuthorInUse(int authorId)
        {
            SqlCommand cmd = new SqlCommand(SqlQueries.IsAuthorInUseQuery, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@AuthorId", authorId);

            DataTable resultTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            using (sqlDataAdapter)
            {
                sqlDataAdapter.Fill(resultTable);
            }

            if (resultTable.Rows.Count != 0)
            {
                return true;
            }

            return false;
        }

        public bool IsAuthorNameInUse(Author author)
        {
            SqlCommand cmd = new SqlCommand(SqlQueries.IsAuthorNameInUseQuery, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@AuthorId", author.Id);
            cmd.Parameters.AddWithValue("@AuthorFirstName", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(author.FirstName.Trim()));
            cmd.Parameters.AddWithValue("@AuthorMiddleName", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(author.MiddleName.Trim()));
            cmd.Parameters.AddWithValue("@AuthorLastName", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(author.LastName.Trim()));

            DataTable resultTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            using (sqlDataAdapter)
            {
                sqlDataAdapter.Fill(resultTable);
            }

            if (resultTable.Rows.Count != 0)
            {
                return true;
            }

            return false;
        }
    }
}
