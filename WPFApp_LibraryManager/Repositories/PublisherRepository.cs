using System.Collections.Generic;
using System.Data;
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
    }
}
