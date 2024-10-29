using System.Configuration;
using System.Data.SqlClient;

namespace WPFApp_LibraryManager.Repositories
{
    internal class PublisherRepository
    {
        private readonly SqlConnection _sqlConnection;

        public PublisherRepository()
        {
            _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["WPFApp_LibraryManager.Properties.Settings.LibraryManagerDBConnectionString"].ConnectionString);
        }
    }
}
