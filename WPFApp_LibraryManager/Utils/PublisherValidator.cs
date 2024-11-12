using System.Windows;
using System;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Utils
{
    internal class PublisherValidator : IPublisherValidator
    {
        public bool IsValidPublisher(Publisher publisher)
        {
            if (!IsValidPublisherName(publisher.Name))
            {
                MessageBox.Show("Name is invalid. Please provide the publisher name.");

                return false;
            }

            return true;
        }

        private bool IsValidPublisherName(string inputString)
        {
            if (String.IsNullOrWhiteSpace(inputString))
            {
                return false;
            }

            return true;
        }
    }
}
