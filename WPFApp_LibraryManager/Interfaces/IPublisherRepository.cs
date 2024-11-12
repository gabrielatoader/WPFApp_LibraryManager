using System.Collections.Generic;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Interfaces
{
    public interface IPublisherRepository
    {
        List<Publisher> GetPublisherList();

        List<Publisher> GetFilteredPublisherList(string searchString);

        void InsertPublisher(Publisher publisher);

        void UpdatePublisher(Publisher publisher);

        void DeletePublisher(Publisher publisher);
    }
}
