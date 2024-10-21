using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFApp_LibraryManager.Models;
using WPFApp_LibraryManager.Utils;

namespace WPFApp_LibraryManager.Services
{
    public class PublisherService
    {
        public List<Publisher> GetPublishers()
        {
            List<Publisher> publisherList = new List<Publisher>();

            Publisher header = new Publisher();

            header.Id = 0;
            header.Name = "- PUBLISHER -";
            publisherList.Add(header);

            DataTable publishersTable = DbContext.GetResultTable(SqlQueries.AllPublishersQuery);

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
