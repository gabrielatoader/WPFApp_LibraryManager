using System.Configuration;
using System.Data.SqlClient;

namespace WPFApp_LibraryManager.Repositories
{
    public class BookRepository
    {
        private readonly SqlConnection _sqlConnection;

        public BookRepository()
        {
            _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["WPFApp_LibraryManager.Properties.Settings.LibraryManagerDBConnectionString"].ConnectionString);
        }
    }
}
