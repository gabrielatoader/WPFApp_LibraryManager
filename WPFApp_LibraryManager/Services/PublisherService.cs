using System;
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

            try
            {
                publisherList = _publisherRepository.GetPublisherList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not get publisher list: {ex.Message}");
            }

            return publisherList;
        }

        public List<Publisher> GetPublisherListWithListHeader()
        {
            List<Publisher> publisherList = new List<Publisher>();

            Publisher header = new Publisher();
            header.Id = 0;
            header.Name = "- PUBLISHER -";
            publisherList.Add(header);

            try
            {
                publisherList.AddRange(_publisherRepository.GetPublisherList());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not get publisher list: {ex.Message}");
            }

            return publisherList;
        }

        public List<Publisher> GetFilteredPublisherList(string searchString)
        {
            List<Publisher> publisherList = new List<Publisher>();

            try
            {
                publisherList = _publisherRepository.GetFilteredPublisherList(searchString);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not get filtered publisher list: {ex.Message}");
            }

            return publisherList;
        }

        public bool InsertPublisher(Publisher publisher)
        {
            if (_publisherValidator.IsValidPublisher(publisher))
            {
                try
                {
                    _publisherRepository.InsertPublisher(publisher);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Could not insert publisher: {ex.Message}");
                }

                MessageBox.Show("Publisher added successfully!");

                return true;
            }

            return false;
        }

        public bool UpdatePublisher(Publisher publisher)
        {
            if (_publisherValidator.IsValidPublisher(publisher))
            {
                try
                {
                    _publisherRepository.UpdatePublisher(publisher);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Could not update publisher: {ex.Message}");
                }

                MessageBox.Show("Publisher updated successfully!");

                return true;
            }

            return false;
        }

        public void DeletePublisher(int publisherId)
        {
            try
            {
                if (_publisherRepository.IsPublisherInUse(publisherId))
                {
                    MessageBox.Show($"Could not delete Publisher #{publisherId}. Some books are still associated with it.");
                }
                else
                {
                    _publisherRepository.DeletePublisher(publisherId);

                    MessageBox.Show($"Publisher #{publisherId} deleted successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not delete Publisher #{publisherId}: {ex.Message}");
            }
        }
    }
}