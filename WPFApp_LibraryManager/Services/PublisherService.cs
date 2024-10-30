using System.Collections.Generic;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Services
{
    public class PublisherService : IPublisherService
    {
        private IPublisherRepository _publisherRepository;

        public PublisherService(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }
        
        public List<Publisher> GetPublishers()
        {
            List<Publisher> publisherList = new List<Publisher>();

            Publisher header = new Publisher();
            header.Id = 0;
            header.Name = "- PUBLISHER -";
            publisherList.Add(header);

            publisherList.AddRange(_publisherRepository.GetPublisherList());

            return publisherList;
        }
    }
}