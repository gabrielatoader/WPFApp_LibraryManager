using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Pages
{
    public partial class PublishersPage : UserControl
    {
        private IPublisherService _publisherService;

        private string _requestType = "";

        public PublishersPage(IPublisherService publisherService)
        {
            _publisherService = publisherService;

            InitializeComponent();

            BindPublisherListToGrid(_publisherService.GetPublisherList());
        }

        private void BindPublisherListToGrid(List<Publisher> publisherList)
        {
            PublisherList_Dtg.ItemsSource = publisherList;
        }

        private void ClearPublisherGrid()
        {
            BindPublisherListToGrid(_publisherService.GetPublisherList());
        }

        private void BindPublisherToPublisherDetails(Publisher publisher)
        {
            TargetPublisher_Name_Txt.Text = publisher.Name;
            TargetPublisher_Description_Txt.Text = publisher.Description;

            DisablePublisherDetails();
        }

        private void Clear_Btn_Click(object sender, RoutedEventArgs e)
        {
            _requestType = "";

            ClearSearch();
            ClearPublisherGrid();
            ClearPublisherDetails();
        }

        private void Save_Btn_Click(object sender, RoutedEventArgs e)
        {
            Publisher targetPublisher = new Publisher();
            targetPublisher.Name = TargetPublisher_Name_Txt.Text;
            targetPublisher.Description = TargetPublisher_Description_Txt.Text;

            bool result = false;

            if (_requestType == "update")
            {
                Publisher activePublisher = (Publisher)PublisherList_Dtg.SelectedItem;

                targetPublisher.Id = activePublisher.Id;

                result = _publisherService.UpdatePublisher(targetPublisher);
            }
            else if (_requestType == "insert")
            {
                result = _publisherService.InsertPublisher(targetPublisher);
            }
            else
            {
                MessageBox.Show("Something went wrong, try again.");
            }

            if (result == true)
            {
                Clear_Btn_Click(sender, e);
            }
        }

        private void Cancel_Btn_Click(object sender, RoutedEventArgs e)
        {
            _requestType = "";

            if ((Publisher)PublisherList_Dtg.SelectedItem != null)
            {
                BindPublisherToPublisherDetails((Publisher)PublisherList_Dtg.SelectedItem);
            }
            else 
            {
                ClearPublisherDetails();
            }
        }

        private void Edit_Btn_Click(object sender, RoutedEventArgs e)
        {
            _requestType = "update";

            EnablePublisherDetails();
        }

        private void Delete_Btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Publisher activePublisher = (Publisher)PublisherList_Dtg.SelectedItem;

                _publisherService.DeletePublisher(activePublisher.Id);

                Clear_Btn_Click(sender, e);
            }
        }

        private void AddPublisher_Btn_Click(object sender, RoutedEventArgs e)
        {
            _requestType = "insert";

            ClearPublisherDetails();

            EnablePublisherDetails();
        }

        private void Search_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(Search_Txt.Text))
            {
                MessageBox.Show("Search box is empty, please add a search string.");
            }
            else
            {
                BindPublisherListToGrid(_publisherService.GetFilteredPublisherList(Search_Txt.Text));
            }
        }

        private void PublisherList_Dtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;

            var selectedRow = dataGrid.SelectedItem as Publisher;

            if (selectedRow != null)
            {
                BindPublisherToPublisherDetails(selectedRow);
            }
        }

        private void ClearSearch()
        {
            Search_Txt.Text = string.Empty;
        }

        private void EnablePublisherDetails()
        {
            TargetPublisher_Name_Txt.IsEnabled = true;
            TargetPublisher_Description_Txt.IsEnabled = true;

            EnableSaveCancelButtons();
            DisableEditDeleteButtons();
        }

        private void DisablePublisherDetails()
        {
            TargetPublisher_Name_Txt.IsEnabled = false;
            TargetPublisher_Description_Txt.IsEnabled = false;

            EnableEditDeleteButtons();
            DisableSaveCancelButtons();
        }

        private void ClearPublisherDetails()
        {
            TargetPublisher_Name_Txt.Text = string.Empty;
            TargetPublisher_Description_Txt.Text = string.Empty;

            DisablePublisherDetails();
            DisableEditDeleteButtons();
        }

        private void EnableSaveCancelButtons()
        {
            Cancel_Btn.IsEnabled = true;
            Save_Btn.IsEnabled = true;
        }

        private void DisableSaveCancelButtons()
        {
            Cancel_Btn.IsEnabled = false;
            Save_Btn.IsEnabled = false;
        }

        private void EnableEditDeleteButtons()
        {
            Edit_Btn.IsEnabled = true;
            Delete_Btn.IsEnabled = true;
        }

        private void DisableEditDeleteButtons()
        {
            Edit_Btn.IsEnabled = false;
            Delete_Btn.IsEnabled = false;
        }
    }
}
