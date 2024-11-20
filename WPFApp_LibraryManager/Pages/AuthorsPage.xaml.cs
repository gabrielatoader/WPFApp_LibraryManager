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

            BindCategoriesToGrid(_authorService.GetAuthorList());
        }

        private void BindCategoriesToGrid(List<Author> authorList)
        {
            AuthorList_Dtg.ItemsSource = authorList;
        }

        private void ResetDataGrid()
        {
            BindCategoriesToGrid(_authorService.GetAuthorList());
        }

        private void BindAuthorToAuthorDetails(Author author)
        {
            TargetAuthor_FirstName_Txt.Text = author.FirstName;
            TargetAuthor_MiddleName_Txt.Text = author.MiddleName;
            TargetAuthor_LastName_Txt.Text = author.LastName;
        }

        private void Clear_Btn_Click(object sender, RoutedEventArgs e)
        {
            _requestType = "";

            ClearSearch();
            ResetDataGrid();
            ClearActiveAuthorSection();
            DisableActiveAuthorSection();

            Edit_Btn.IsEnabled = false;
            Delete_Btn.IsEnabled = false;
            Save_Btn.IsEnabled = false;
            Cancel_Btn.IsEnabled = false;
            AddAuthor_Btn.IsEnabled = true;
        }

        private void Save_Btn_Click(object sender, RoutedEventArgs e)
        {
            Author targetAuthor = new Author();
            targetAuthor.FirstName = TargetAuthor_FirstName_Txt.Text;
            targetAuthor.MiddleName = TargetAuthor_MiddleName_Txt.Text;
            targetAuthor.LastName = TargetAuthor_LastName_Txt.Text;

            if (_requestType == "update")
            {
                Author activeAuthor = (Author)AuthorList_Dtg.SelectedItem;
                targetAuthor.Id = activeAuthor.Id;

                bool result = _authorService.UpdateAuthor(targetAuthor);

                if (result == true)
                {
                    Clear_Btn_Click(sender, e);

                    BindAuthorToAuthorDetails(targetAuthor);
                }
            }
            else if (_requestType == "insert")
            {
                bool result = _authorService.InsertAuthor(targetAuthor);

                if (result == true)
                {
                    Clear_Btn_Click(sender, e);

                    BindAuthorToAuthorDetails(targetAuthor);
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

            if ((Author)AuthorList_Dtg.SelectedItem != null)
            {
                BindAuthorToAuthorDetails((Author)AuthorList_Dtg.SelectedItem);
                Delete_Btn.IsEnabled = true;
                Edit_Btn.IsEnabled = true;
            }

            DisableActiveAuthorSection();

            Cancel_Btn.IsEnabled = false;
            Save_Btn.IsEnabled = false;
            AddAuthor_Btn.IsEnabled = true;
        }

        private void Edit_Btn_Click(object sender, RoutedEventArgs e)
        {
            _requestType = "update";

            EnableActiveAuthorSection();

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
                Author activeAuthor = (Author)AuthorList_Dtg.SelectedItem;

                _authorService.DeleteAuthor(activeAuthor.Id);

                Clear_Btn_Click(sender, e);
            }
        }

        private void AddAuthor_Btn_Click(object sender, RoutedEventArgs e)
        {
            _requestType = "insert";

            ClearActiveAuthorSection();
            EnableActiveAuthorSection();

            AddAuthor_Btn.IsEnabled = false;
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
                BindCategoriesToGrid(_authorService.GetFilteredAuthorList(Search_Txt.Text));
            }
        }

        private void AuthorList_Dtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            var selectedRow = dataGrid.SelectedItem as Author;

            if (selectedRow != null)
            {
                BindAuthorToAuthorDetails(selectedRow);

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

        private void EnableActiveAuthorSection()
        {
            TargetAuthor_FirstName_Txt.IsEnabled = true;
            TargetAuthor_MiddleName_Txt.IsEnabled = true;
            TargetAuthor_LastName_Txt.IsEnabled = true;
        }

        private void DisableActiveAuthorSection()
        {
            TargetAuthor_FirstName_Txt.IsEnabled = false;
            TargetAuthor_MiddleName_Txt.IsEnabled = false;
            TargetAuthor_LastName_Txt.IsEnabled = false;
        }

        private void ClearActiveAuthorSection()
        {
            TargetAuthor_FirstName_Txt.Text = string.Empty;
            TargetAuthor_MiddleName_Txt.Text = string.Empty;
            TargetAuthor_LastName_Txt.Text = string.Empty;
        }
    }
}