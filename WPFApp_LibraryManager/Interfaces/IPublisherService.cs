using System.Collections.Generic;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Interfaces
{
    public interface IPublisherService
    {
        List<Publisher> GetPublisherList();
        
        List<Publisher> GetPublisherListWithListHeader();

        List<Publisher> GetFilteredPublisherList(string searchString);

        bool InsertPublisher(Publisher publisher);

        bool UpdatePublisher(Publisher publisher);

        void DeletePublisher(int publisherId);
    }
}
