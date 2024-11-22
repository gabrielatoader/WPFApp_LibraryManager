using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Pages
{
    public partial class AuthorsPage : UserControl
    {
        private IAuthorService _authorService;

        private string _requestType = "";

        public AuthorsPage(IAuthorService authorService)
        {
            _authorService = authorService;

            InitializeComponent();

            BindAuthorListToGrid(_authorService.GetAuthorList());
        }

        private void BindAuthorListToGrid(List<Author> authorList)
        {
            AuthorList_Dtg.ItemsSource = authorList;
        }

        private void ClearAuthorGrid()
        {
            BindAuthorListToGrid(_authorService.GetAuthorList());
        }

        private void BindAuthorToAuthorDetails(Author author)
        {
            TargetAuthor_FirstName_Txt.Text = author.FirstName;
            TargetAuthor_MiddleName_Txt.Text = author.MiddleName;
            TargetAuthor_LastName_Txt.Text = author.LastName;

            DisableAuthorDetails();
        }

        private void Clear_Btn_Click(object sender, RoutedEventArgs e)
        {
            _requestType = "";

            ClearSearch();
            ClearAuthorGrid();
            ClearAuthorDetails();
        }

        private void Save_Btn_Click(object sender, RoutedEventArgs e)
        {
            Author targetAuthor = new Author();
            targetAuthor.FirstName = TargetAuthor_FirstName_Txt.Text;
            targetAuthor.MiddleName = TargetAuthor_MiddleName_Txt.Text;
            targetAuthor.LastName = TargetAuthor_LastName_Txt.Text;

            bool result = false;

            if (_requestType == "update")
            {
                Author activeAuthor = (Author)AuthorList_Dtg.SelectedItem;

                targetAuthor.Id = activeAuthor.Id;

                result = _authorService.UpdateAuthor(targetAuthor);
            }
            else if (_requestType == "insert")
            {
                result = _authorService.InsertAuthor(targetAuthor);
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

            if ((Author)AuthorList_Dtg.SelectedItem != null)
            {
                BindAuthorToAuthorDetails((Author)AuthorList_Dtg.SelectedItem);
            }
            else
            {
                ClearAuthorDetails();
            }
        }

        private void Edit_Btn_Click(object sender, RoutedEventArgs e)
        {
            _requestType = "update";

            EnableAuthorDetails();
        }

        private void Delete_Btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Author activeAuthor = (Author)AuthorList_Dtg.SelectedItem;

                _authorService.DeleteAuthor(activeAuthor.Id);

                Clear_Btn_Click(sender, e);
            }
        }

        private void AddAuthor_Btn_Click(object sender, RoutedEventArgs e)
        {
            _requestType = "insert";

            ClearAuthorDetails();
            
            EnableAuthorDetails();
        }

        private void Search_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(Search_Txt.Text))
            {
                MessageBox.Show("Search box is empty, please add a search string.");
            }
            else
            {
                BindAuthorListToGrid(_authorService.GetFilteredAuthorList(Search_Txt.Text));
            }
        }

        private void AuthorList_Dtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;

            var selectedRow = dataGrid.SelectedItem as Author;

            if (selectedRow != null)
            {
                BindAuthorToAuthorDetails(selectedRow);
            }
        }

        private void ClearSearch()
        {
            Search_Txt.Text = string.Empty;
        }

        private void EnableAuthorDetails()
        {
            TargetAuthor_FirstName_Txt.IsEnabled = true;
            TargetAuthor_MiddleName_Txt.IsEnabled = true;
            TargetAuthor_LastName_Txt.IsEnabled = true;

            EnableSaveCancelButtons();
            DisableEditDeleteButtons();
        }

        private void DisableAuthorDetails()
        {
            TargetAuthor_FirstName_Txt.IsEnabled = false;
            TargetAuthor_MiddleName_Txt.IsEnabled = false;
            TargetAuthor_LastName_Txt.IsEnabled = false;

            EnableEditDeleteButtons();
            DisableSaveCancelButtons();
        }

        private void ClearAuthorDetails()
        {
            TargetAuthor_FirstName_Txt.Text = string.Empty;
            TargetAuthor_MiddleName_Txt.Text = string.Empty;
            TargetAuthor_LastName_Txt.Text = string.Empty;

            DisableAuthorDetails();
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