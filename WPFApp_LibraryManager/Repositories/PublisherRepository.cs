using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;
using WPFApp_LibraryManager.Utils;

namespace WPFApp_LibraryManager.Repositories
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly SqlConnection _sqlConnection;

        public PublisherRepository()
        {
            _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["WPFApp_LibraryManager.Properties.Settings.LibraryManagerDBConnectionString"].ConnectionString);
        }

        public List<Publisher> GetPublisherList()
        {
            List<Publisher> publisherList = new List<Publisher>();

            DataTable publishersTable = GetResultTable(SqlQueries.AllPublishersQuery);

            foreach (DataRow publisherRow in publishersTable.Rows)
            {
                Publisher publisher = new Publisher();
                publisher.Id = (int)publisherRow["PublisherId"];
                publisher.Name = (string)publisherRow["PublisherName"];

                publisherList.Add(publisher);
            }

            return publisherList;
        }
        
        private DataTable GetResultTable(string query)
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
