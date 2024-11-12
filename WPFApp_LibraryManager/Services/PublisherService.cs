using System.Collections.Generic;
using System.Windows;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Services
{
    public class PublisherService : IPublisherService
    {
        private IPublisherRepository _publisherRepository;

        private IPublisherValidator _publisherValidator;

        public PublisherService(IPublisherRepository publisherRepository, IPublisherValidator publisherValidator)
        {
            _publisherRepository = publisherRepository;
            _publisherValidator = publisherValidator;
        }

        public List<Publisher> GetPublisherList()
        {
            List<Publisher> publisherList = new List<Publisher>();

            publisherList.AddRange(_publisherRepository.GetPublisherList());

            return publisherList;
        }

        public List<Publisher> GetPublisherListWithListHeader()
        {
            List<Publisher> publisherList = new List<Publisher>();

            Publisher header = new Publisher();
            header.Id = 0;
            header.Name = "- PUBLISHER -";
            publisherList.Add(header);

            publisherList.AddRange(_publisherRepository.GetPublisherList());

            return publisherList;
        }

        public List<Publisher> GetFilteredPublisherList(string searchString)
        {
            List<Publisher> publisherList = new List<Publisher>();

            publisherList.AddRange(_publisherRepository.GetFilteredPublisherList(searchString));

            return publisherList;
        }

        public bool InsertPublisher(Publisher publisher)
        {
            if (_publisherValidator.IsValidPublisher(publisher))
            {
                _publisherRepository.InsertPublisher(publisher);

                MessageBox.Show("Publisher added successfully!");

                return true;
            }
            return false;
        }

        public bool UpdatePublisher(Publisher publisher)
        {
            if (_publisherValidator.IsValidPublisher(publisher))
            {
                _publisherRepository.UpdatePublisher(publisher);

                MessageBox.Show("Publisher updated successfully!");

                return true;
            }
            return false;
        }

        public void DeletePublisher(Publisher publisher)
        {
            _publisherRepository.DeletePublisher(publisher);

            MessageBox.Show("Publisher deleted successfully!");
        }
    }
}