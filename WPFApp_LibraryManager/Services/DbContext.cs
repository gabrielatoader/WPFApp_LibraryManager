using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Services
{
    public static class DbContext
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["WPFApp_LibraryManager.Properties.Settings.LibraryManagerDBConnectionString"].ConnectionString;
        private static SqlConnection _sqlConnection = new SqlConnection(connectionString);
        
        public static DataTable GetResultTable(string query)
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, _sqlConnection);

            using (sqlDataAdapter)
            {
                DataTable resultsTable = new DataTable();

                sqlDataAdapter.Fill(resultsTable);

                return resultsTable;
            }
        }
        public static DataTable GetResultTableFilteredByAuthor(string query, int authorId)
        {
            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@AuthorId", authorId);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            using (sqlDataAdapter)
            {
                DataTable resultsTable = new DataTable();

                sqlDataAdapter.Fill(resultsTable);

                return resultsTable;
            }
        }

        public static DataTable GetResultTableFilteredByPublisher(string query, int publisherId)
        {
            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@PublisherId", publisherId);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            using (sqlDataAdapter)
            {
                DataTable resultsTable = new DataTable();

                sqlDataAdapter.Fill(resultsTable);

                return resultsTable;
            }
        }
        public static DataTable GetResultTableFilteredByCategory(string query, int categoryId)
        {
            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@CategoryId", categoryId);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            using (sqlDataAdapter)
            {
                DataTable resultsTable = new DataTable();

                sqlDataAdapter.Fill(resultsTable);

                return resultsTable;
            }
        }
        public static void InsertBookInDb(string query, Book book)
        {
            _sqlConnection.Open();

            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Title", book.Title);
            cmd.Parameters.AddWithValue("@ISBN", book.ISBN);
            cmd.Parameters.AddWithValue("@AuthorId", book.AuthorId);
            cmd.Parameters.AddWithValue("@PublisherId", book.PublisherId);
            cmd.Parameters.AddWithValue("@PublishedYear", book.PublishedYear);
            cmd.Parameters.AddWithValue("@CategoryId", book.CategoryId);
            cmd.Parameters.AddWithValue("@CoverURL", book.CoverURL);

            cmd.ExecuteNonQuery();

            _sqlConnection.Close();
        }
    }
}