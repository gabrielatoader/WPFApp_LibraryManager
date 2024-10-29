using System.Configuration;
using System.Data.SqlClient;

namespace WPFApp_LibraryManager.Repositories
{
    internal class CategoryRepository
    {
        private readonly SqlConnection _sqlConnection;

        public CategoryRepository()
        {
            _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["WPFApp_LibraryManager.Properties.Settings.LibraryManagerDBConnectionString"].ConnectionString);
        }
    }
}
