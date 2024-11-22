using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;
using WPFApp_LibraryManager.Utils;

namespace WPFApp_LibraryManager.Repositories
{
    public class PublisherRepository : BaseRepository, IPublisherRepository
    {
        public List<Publisher> GetPublisherList()
        {
            List<Publisher> publisherList = new List<Publisher>();

            DataTable publisherTable = GetResultTable(SqlQueries.GetPublisherListQuery);

            foreach (DataRow publisherRow in publisherTable.Rows)
            {
                Publisher publisher = new Publisher();
                publisher.Id = (int)publisherRow["PublisherId"];
                publisher.Name = (string)publisherRow["PublisherName"];
                publisher.Description = (string)publisherRow["PublisherDescription"];

                publisherList.Add(publisher);
            }

            return publisherList;
        }

        public List<Publisher> GetFilteredPublisherList(string searchString)
        {
            SqlCommand cmd = new SqlCommand(SqlQueries.GetFilteredPublisherListQuery, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@SearchString", searchString);

            DataTable publisherTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            using (sqlDataAdapter)
            {
                sqlDataAdapter.Fill(publisherTable);
            }

            List<Publisher> publisherList = new List<Publisher>();

            foreach (DataRow publisherRow in publisherTable.Rows)
            {
                Publisher publisher = new Publisher();
                publisher.Id = (int)publisherRow["PublisherId"];
                publisher.Name = (string)publisherRow["PublisherName"];
                publisher.Description = (string)publisherRow["PublisherDescription"];

                publisherList.Add(publisher);
            }

            return publisherList;
        }

        public void InsertPublisher(Publisher publisher)
        {
            _sqlConnection.Open();

            SqlCommand cmd = new SqlCommand(SqlQueries.InsertPublisherQuery, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@PublisherName", publisher.Name);
            cmd.Parameters.AddWithValue("@PublisherDescription", publisher.Description);

            cmd.ExecuteNonQuery();

            _sqlConnection.Close();
        }

        public void UpdatePublisher(Publisher publisher)
        {
            _sqlConnection.Open();

            SqlCommand cmd = new SqlCommand(SqlQueries.UpdatePublisherQuery, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@PublisherId", publisher.Id);
            cmd.Parameters.AddWithValue("@PublisherName", publisher.Name);
            cmd.Parameters.AddWithValue("@PublisherDescription", publisher.Description);

            cmd.ExecuteNonQuery();

            _sqlConnection.Close();
        }

        public void DeletePublisher(int publisherId)
        {
            _sqlConnection.Open();

            SqlCommand cmd = new SqlCommand(SqlQueries.DeletePublisherQuery, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@PublisherId", publisherId);

            cmd.ExecuteNonQuery();

            _sqlConnection.Close();
        }
    }
}
