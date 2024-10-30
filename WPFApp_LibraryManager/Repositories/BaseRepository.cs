using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WPFApp_LibraryManager.Repositories
{
    public class BaseRepository
    {
        protected readonly SqlConnection _sqlConnection;

        public BaseRepository()
        {
            _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["WPFApp_LibraryManager.Properties.Settings.LibraryManagerDBConnectionString"].ConnectionString);
        }
        
        protected DataTable GetResultTable(string query)
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, _sqlConnection);

            using (sqlDataAdapter)
            {
                DataTable resultsTable = new DataTable();

                sqlDataAdapter.Fill(resultsTable);

                return resultsTable;
            }
        }
    }
}
