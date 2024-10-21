using System.Data.SqlClient;
using System.Data;
using System.Windows.Controls;
using System.Configuration;
using WPFApp_LibraryManager.Components;
using WPFApp_LibraryManager.Services;
using System.Windows;
using System.Collections.Generic;
using WPFApp_LibraryManager.Models;
using System;
using System.Windows.Documents;
using System.Windows.Media;
using System.Linq;
using System.Windows.Media.Imaging;
using WPFApp_LibraryManager.Utils;

namespace WPFApp_LibraryManager.Pages
{
    /// <summary>
    /// Interaction logic for BooksPage.xaml
    /// </summary>
    public partial class BooksPage : UserControl
    {
        private AuthorService _authorService;
        private PublisherService _publisherService;
        private BookService _bookService;
        private CategoryService _categoryService;

        public BooksPage()
        {
            InitializeComponent();
            _authorService = new AuthorService();
            _publisherService = new PublisherService();
            _bookService = new BookService();
            _categoryService = new CategoryService();
            Test_DBService test_DBService = new Test_DBService();
            SqlTest.Text = test_DBService.TestSqlQuery();
            BindBooksToGrid(_bookService.GetAllBooksList());
            BindAuthorsToCbo(AuthorFilter_Cbo, 0);
            BindPublishersToCbo(PublisherFilter_Cbo, 0);
            BindCategoriesToCbo(CategoryFilter_Cbo, 0);
        }

        public void BindAuthorsToCbo(ComboBox cbo, int selectedIndex)
        {

            List<Author> authorList = _authorService.GetAuthors();

            cbo.ItemsSource = authorList;
            cbo.DisplayMemberPath = "FullName";
            cbo.SelectedValuePath = "Id";
            cbo.SelectedIndex = selectedIndex;

        }

        public void BindPublishersToCbo(ComboBox cbo, int selectedIndex)
        {

            List<Publisher> publisherList = _publisherService.GetPublishers();

            cbo.ItemsSource = publisherList;
            cbo.DisplayMemberPath = "Name";
            cbo.SelectedValuePath = "Id";
            cbo.SelectedIndex = selectedIndex;

        }

        public void BindCategoriesToCbo(ComboBox cbo, int selectedIndex)
        {

            List<Category> categoryList = _categoryService.GetCategories();

            cbo.ItemsSource = categoryList;
            cbo.DisplayMemberPath = "Name";
            cbo.SelectedValuePath = "Id";
            cbo.SelectedIndex = selectedIndex;

        }

        public void BindBooksToGrid(List<Book> bookList)
        {
            BookList_Dtg.ItemsSource = bookList;
        }

        private void Clear_Btn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ClearFilters();
            ClearSearch();
            ClearDataGrid();
            ClearBookDetails();
            Edit_Btn.IsEnabled = false;
            Delete_Btn.IsEnabled = false;
            Save_Btn.IsEnabled = false;
            Cancel_Btn.IsEnabled = false;
        }

        private void BookList_Dtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            var selectedRow = dataGrid.SelectedItem as Book;
            if (selectedRow != null)
            {
                SelectedBook_Title_Txt.Text = selectedRow.Title;
                SelectedBook_ISBN_Txt.Text = selectedRow.ISBN;

                BindAuthorsToCbo(SelectedBook_Author_Cbo, selectedRow.AuthorId);
                SelectedBook_Category_Cbo.Text = selectedRow.CategoryName;
                BindPublishersToCbo(SelectedBook_Publisher_Cbo, selectedRow.PublisherId);
                SelectedBook_Published_Year_Txt.Text = selectedRow.PublishedYear.ToString();

                ImageSourceConverter coverConverter = new ImageSourceConverter();

                SelectedBook_Cover_img.Source = (ImageSource)coverConverter.ConvertFromString(selectedRow.CoverURL);

                Edit_Btn.IsEnabled = true;
                Delete_Btn.IsEnabled = true;
                Save_Btn.IsEnabled = false;
                Cancel_Btn.IsEnabled = false;
            }
        }

        private void AuthorFilter_Cbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AuthorFilter_Cbo.SelectedIndex > 0)
            {
                BindBooksToGrid(_bookService.GetFilteredBooksByAuthor(AuthorFilter_Cbo.SelectedIndex));
            }
        }
        
        private void PublisherFilter_Cbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PublisherFilter_Cbo.SelectedIndex > 0)
            {
                BindBooksToGrid(_bookService.GetFilteredBooksByPublisher(PublisherFilter_Cbo.SelectedIndex));
            }
        }

        private void CategoryFilter_Cbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryFilter_Cbo.SelectedIndex > 0)
            {
                BindBooksToGrid(_bookService.GetFilteredBooksByCategory(CategoryFilter_Cbo.SelectedIndex));
            }
        }

        private void Edit_Btn_Click(object sender, RoutedEventArgs e)
        {
            EnableBookDetails();
            Edit_Btn.IsEnabled = false;
            Delete_Btn.IsEnabled = false;
            Cancel_Btn.IsEnabled = true;
            Save_Btn.IsEnabled = true;
        }

        private void EnableBookDetails()
        {
            SelectedBook_Title_Txt.IsEnabled = true;
            SelectedBook_ISBN_Txt.IsEnabled = true;
            SelectedBook_Author_Cbo.IsEnabled = true;
            SelectedBook_Category_Cbo.IsEnabled = true;
            SelectedBook_Publisher_Cbo.IsEnabled = true;
            SelectedBook_Published_Year_Txt.IsEnabled = true;
        }
        private void DisableBookDetails()
        {
            SelectedBook_Title_Txt.IsEnabled = false;
            SelectedBook_ISBN_Txt.IsEnabled = false;
            SelectedBook_Author_Cbo.IsEnabled = false;
            SelectedBook_Category_Cbo.IsEnabled = false;
            SelectedBook_Publisher_Cbo.IsEnabled = false;
            SelectedBook_Published_Year_Txt.IsEnabled = false;
        }

        private void ClearBookDetails()
        {
            SelectedBook_Title_Txt.Text = string.Empty;
            SelectedBook_ISBN_Txt.Text = string.Empty;
            SelectedBook_Author_Cbo.Text = string.Empty;
            SelectedBook_Category_Cbo.Text = string.Empty;
            SelectedBook_Publisher_Cbo.Text = string.Empty;
            SelectedBook_Published_Year_Txt.Text = string.Empty;
            SelectedBook_Cover_img.Source = new BitmapImage(new Uri(@"../Images/CoverPlaceholder.png", UriKind.Relative));
        }

        private void ClearFilters()
        {
            AuthorFilter_Cbo.SelectedIndex = 0;
            PublisherFilter_Cbo.SelectedIndex = 0;
            CategoryFilter_Cbo.SelectedIndex = 0;
        }

        private void ClearSearch()
        {
            Title_Chk.IsChecked = false;
            Author_Chk.IsChecked = false;
            Publisher_Chk.IsChecked = false;
            ISBN_Chk.IsChecked = false;
            Category_Chk.IsChecked = false;
            Search_Txt.Text = string.Empty;
        }

        private void ClearDataGrid()
        {
            BindBooksToGrid(_bookService.GetAllBooksList());
        }

        private void Cancel_Btn_Click(object sender, RoutedEventArgs e)
        {
            DisableBookDetails();
            Cancel_Btn.IsEnabled = false;
            Delete_Btn.IsEnabled = true;
            Edit_Btn.IsEnabled = true;
            Save_Btn.IsEnabled = false;
        }

        private void Delete_Btn_Click(object sender, RoutedEventArgs e)
        {
            //ToDo: delete from table(s)

        }

        private void Save_Btn_Click(object sender, RoutedEventArgs e)
        {
            //ToDo: update table(s)
            DisableBookDetails();
            Save_Btn.IsEnabled = false;
            Delete_Btn.IsEnabled = true;
            Edit_Btn.IsEnabled = true;
            Cancel_Btn.IsEnabled = false;

        }

        
    }
}
