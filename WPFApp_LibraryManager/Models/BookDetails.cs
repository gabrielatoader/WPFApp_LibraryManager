using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp_LibraryManager.Models
{
    public class BookDetails
    {

        public int BookId { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public int PublishedYear { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CoverURL { get; set; }
    }
}
