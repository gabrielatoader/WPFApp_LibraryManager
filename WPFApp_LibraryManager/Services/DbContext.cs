using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFApp_LibraryManager.Models;
using WPFApp_LibraryManager.Utils;

namespace WPFApp_LibraryManager.Services
{
    public static class DbContext
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["WPFApp_LibraryManager.Properties.Settings.LibraryManagerDBConnectionString"].ConnectionString;
        // ToDo: make private after you create all other services
        
        private static SqlConnection _sqlConnection = new SqlConnection(connectionString);
        
        //ToDo: remove after making private
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(connectionString);
        }

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
            SqlCommand cmd = new SqlCommand(query, DbContext.GetSqlConnection());
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
            SqlCommand cmd = new SqlCommand(query, DbContext.GetSqlConnection());
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
            SqlCommand cmd = new SqlCommand(query, DbContext.GetSqlConnection());
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

        public static DataRow GetResultRow(string query)
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, _sqlConnection);

            using (sqlDataAdapter)
            {
                DataTable resultsTable = new DataTable();

                sqlDataAdapter.Fill(resultsTable);

                return resultsTable.Rows[0];
            }
        }

    }
}
