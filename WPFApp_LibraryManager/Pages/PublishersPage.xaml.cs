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

            BindPublishersToGrid(_publisherService.GetPublisherList());
        }

        private void BindPublishersToGrid(List<Publisher> publisherList)
        {
            PublisherList_Dtg.ItemsSource = publisherList;
        }

        private void ResetDataGrid()
        {
            BindPublishersToGrid(_publisherService.GetPublisherList());
        }

        private void BindPublisherToPublisherDetails(Publisher publisher)
        {
            TargetPublisher_Name_Txt.Text = publisher.Name;
            TargetPublisher_Description_Txt.Text = publisher.Description;
        }

        private void Clear_Btn_Click(object sender, RoutedEventArgs e)
        {
            _requestType = "";

            ClearSearch();
            ResetDataGrid();
            ClearActivePublisherSection();
            DisableActivePublisherSection();

            Edit_Btn.IsEnabled = false;
            Delete_Btn.IsEnabled = false;
            Save_Btn.IsEnabled = false;
            Cancel_Btn.IsEnabled = false;
            AddPublisher_Btn.IsEnabled = true;
        }

        private void Save_Btn_Click(object sender, RoutedEventArgs e)
        {
            Publisher targetPublisher = new Publisher();
            targetPublisher.Name = TargetPublisher_Name_Txt.Text;
            targetPublisher.Description = TargetPublisher_Description_Txt.Text;

            if (_requestType == "update")
            {
                Publisher activePublisher = (Publisher)PublisherList_Dtg.SelectedItem;
                targetPublisher.Id = activePublisher.Id;

                bool result = _publisherService.UpdatePublisher(targetPublisher);

                if (result == true)
                {
                    Clear_Btn_Click(sender, e);

                    BindPublisherToPublisherDetails(targetPublisher);
                }
            }
            else if (_requestType == "insert")
            {
                bool result = _publisherService.InsertPublisher(targetPublisher);

                if (result == true)
                {
                    Clear_Btn_Click(sender, e);

                    BindPublisherToPublisherDetails(targetPublisher);
                }
            }
            else
            {
                MessageBox.Show("Something went wrong, try again.");
            }
        }

        private void Cancel_Btn_Click(object sender, RoutedEventArgs e)
        {
            _requestType = "";

            if ((Publisher)PublisherList_Dtg.SelectedItem != null)
            {
                BindPublisherToPublisherDetails((Publisher)PublisherList_Dtg.SelectedItem);
                Delete_Btn.IsEnabled = true;
                Edit_Btn.IsEnabled = true;
            }

            DisableActivePublisherSection();

            Cancel_Btn.IsEnabled = false;
            Save_Btn.IsEnabled = false;
            AddPublisher_Btn.IsEnabled = true;
        }

        private void Edit_Btn_Click(object sender, RoutedEventArgs e)
        {
            _requestType = "update";

            EnableActivePublisherSection();

            Edit_Btn.IsEnabled = false;
            Delete_Btn.IsEnabled = false;
            Save_Btn.IsEnabled = true;
            Cancel_Btn.IsEnabled = true;
        }

        private void Delete_Btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Publisher activePublisher= (Publisher)PublisherList_Dtg.SelectedItem;

                _publisherService.DeletePublisher(activePublisher);

                Clear_Btn_Click(sender, e);
            }
        }

        private void AddPublisher_Btn_Click(object sender, RoutedEventArgs e)
        {
            _requestType = "insert";

            ClearActivePublisherSection();
            EnableActivePublisherSection();

            AddPublisher_Btn.IsEnabled = false;
            Edit_Btn.IsEnabled = false;
            Delete_Btn.IsEnabled = false;
            Cancel_Btn.IsEnabled = true;
            Save_Btn.IsEnabled = true;
        }

        private void Search_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(Search_Txt.Text))
            {
                MessageBox.Show("Search box is empty, please add a search string.");
            }
            else
            { 
                BindPublishersToGrid(_publisherService.GetFilteredPublisherList(Search_Txt.Text));
            }
        }

        private void PublisherList_Dtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            var selectedRow = dataGrid.SelectedItem as Publisher;

            if (selectedRow != null)
            {
                BindPublisherToPublisherDetails(selectedRow);

                Edit_Btn.IsEnabled = true;
                Delete_Btn.IsEnabled = true;
                Save_Btn.IsEnabled = false;
                Cancel_Btn.IsEnabled = false;
            }
        }

        private void ClearSearch()
        {
            Search_Txt.Text = string.Empty;
        }

        private void EnableActivePublisherSection()
        {
            TargetPublisher_Name_Txt.IsEnabled = true;
            TargetPublisher_Description_Txt.IsEnabled = true;
        }

        private void DisableActivePublisherSection()
        {
            TargetPublisher_Name_Txt.IsEnabled = false;
            TargetPublisher_Description_Txt.IsEnabled = false;
        }

        private void ClearActivePublisherSection()
        {
            TargetPublisher_Name_Txt.Text = string.Empty;
            TargetPublisher_Description_Txt.Text = string.Empty;
        }
    }
}
