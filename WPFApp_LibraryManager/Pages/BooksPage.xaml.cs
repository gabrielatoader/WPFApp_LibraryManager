using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Pages
{
    public partial class BooksPage : UserControl
    {
        private IAuthorService _authorService;
        private IPublisherService _publisherService;
        private IBookService _bookService;
        private ICategoryService _categoryService;
        private string _requestType = "";

        public BooksPage(IAuthorService authorService, IBookService bookService, ICategoryService categoryService, IPublisherService publisherService)
        {
            _authorService = authorService;
            _publisherService = publisherService;
            _bookService = bookService;
            _categoryService = categoryService;

            InitializeComponent();
            
            // bind data to targetBook collection display and search areas
            BindBooksToGrid(_bookService.GetAllBooksList());
            BindAuthorsToCbo(AuthorFilter_Cbo, 0);
            BindPublishersToCbo(PublisherFilter_Cbo, 0);
            BindCategoriesToCbo(CategoryFilter_Cbo, 0);
            
            // bind data to active targetBook display area
            BindAuthorsToCbo(TargetBook_Author_Cbo, 0);
            BindCategoriesToCbo(TargetBook_Category_Cbo, 0);
            BindPublishersToCbo(TargetBook_Publisher_Cbo, 0);

            DisableBookDetails();
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
            List<Category> categoryList = _categoryService.GetCategoryListWithListHeader();

            cbo.ItemsSource = categoryList;
            cbo.DisplayMemberPath = "Name";
            cbo.SelectedValuePath = "Id";
            cbo.SelectedIndex = selectedIndex;
        }

        public void BindBooksToGrid(List<Book> bookList)
        {
            BookList_Dtg.ItemsSource = bookList;
        }
        
        private void BindBookToBookDetails(Book book)
        {
            TargetBook_Title_Txt.Text = book.Title;
            TargetBook_ISBN_Txt.Text = book.ISBN;
            TargetBook_PublishedYear_Txt.Text = book.PublishedYear.ToString();
            TargetBook_CoverURL_Txt.Text = book.CoverURL;
            TargetBook_Author_Cbo.SelectedValue = book.AuthorId;
            TargetBook_Category_Cbo.SelectedValue = book.CategoryId;
            TargetBook_Publisher_Cbo.SelectedValue = book.PublisherId;
            
            try
            {
                TargetBook_Cover_img.Source = new BitmapImage(new Uri(book.CoverURL, UriKind.RelativeOrAbsolute));
            }
            catch
            {
                TargetBook_Cover_img.Source = new BitmapImage(new Uri(@"../Images/CoverPlaceholder.png", UriKind.RelativeOrAbsolute));
            }
        }
        
        private void Clear_Btn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _requestType = "";

            ClearFilters();
            ClearSearch();
            ClearDataGrid();
            ClearBookDetails();
            Edit_Btn.IsEnabled = false;
            Delete_Btn.IsEnabled = false;
            Save_Btn.IsEnabled = false;
            Cancel_Btn.IsEnabled = false;
            AddBook_Btn.IsEnabled = true;
        }
        
        private void BookList_Dtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            var selectedRow = dataGrid.SelectedItem as Book;

            if (selectedRow != null)
            {
                BindBookToBookDetails(selectedRow);

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
            _requestType = "update";

            EnableBookDetails();
            Edit_Btn.IsEnabled = false;
            Delete_Btn.IsEnabled = false;
            Cancel_Btn.IsEnabled = true;
            Save_Btn.IsEnabled = true;
        }

        private void EnableBookDetails()
        {
            TargetBook_Title_Txt.IsEnabled = true;
            TargetBook_ISBN_Txt.IsEnabled = true;
            TargetBook_Author_Cbo.IsEnabled = true;
            TargetBook_Category_Cbo.IsEnabled = true;
            TargetBook_Publisher_Cbo.IsEnabled = true;
            TargetBook_PublishedYear_Txt.IsEnabled = true;
            TargetBook_CoverURL_Txt.IsEnabled = true;
            TargetBook_Cover_img.IsEnabled = true;
        }

        private void DisableBookDetails()
        {
            TargetBook_Title_Txt.IsEnabled = false;
            TargetBook_ISBN_Txt.IsEnabled = false;
            TargetBook_Author_Cbo.IsEnabled = false;
            TargetBook_Category_Cbo.IsEnabled = false;
            TargetBook_Publisher_Cbo.IsEnabled = false;
            TargetBook_PublishedYear_Txt.IsEnabled = false;
            TargetBook_CoverURL_Txt.IsEnabled = false;
            TargetBook_Cover_img.IsEnabled = false;
        }

        private void ClearBookDetails()
        {
            TargetBook_Title_Txt.Text = string.Empty;
            TargetBook_ISBN_Txt.Text = string.Empty;
            TargetBook_Author_Cbo.Text = string.Empty;
            TargetBook_Category_Cbo.Text = string.Empty;
            TargetBook_Publisher_Cbo.Text = string.Empty;
            TargetBook_PublishedYear_Txt.Text = string.Empty;
            TargetBook_CoverURL_Txt.Text = string.Empty;
            TargetBook_Cover_img.Source = new BitmapImage(new Uri(@"../Images/CoverPlaceholder.png", UriKind.Relative));
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
            _requestType = "";

            DisableBookDetails();
            Cancel_Btn.IsEnabled = false;
            Save_Btn.IsEnabled = false;
            Delete_Btn.IsEnabled = true;
            Edit_Btn.IsEnabled = true;
            AddBook_Btn.IsEnabled = true;
        }

        private void Delete_Btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Book activeBook = (Book)BookList_Dtg.SelectedItem;

                _bookService.DeleteBook(activeBook);

                Clear_Btn_Click(sender, e);
            }
        }

        private void Save_Btn_Click(object sender, RoutedEventArgs e)
        {
            
            Book targetBook = new Book();
            targetBook.Title = TargetBook_Title_Txt.Text;
            targetBook.ISBN = TargetBook_ISBN_Txt.Text;
            targetBook.AuthorName = TargetBook_Author_Cbo.Text;
            targetBook.AuthorId = Convert.ToInt32(TargetBook_Author_Cbo.SelectedValue);
            targetBook.CategoryName = TargetBook_Category_Cbo.Text;
            targetBook.CategoryId = Convert.ToInt32(TargetBook_Category_Cbo.SelectedValue);
            targetBook.PublisherName = TargetBook_Publisher_Cbo.Text;
            targetBook.PublisherId = Convert.ToInt32(TargetBook_Publisher_Cbo.SelectedValue);
            targetBook.CoverURL = TargetBook_CoverURL_Txt.Text;

            int year = -1;
            Int32.TryParse(TargetBook_PublishedYear_Txt.Text, out year);
            targetBook.PublishedYear = year;
            

            if (_requestType == "update")
            {
                Book activeBook = (Book)BookList_Dtg.SelectedItem;
                targetBook.BookId = activeBook.BookId;

                bool result = _bookService.UpdateBook(targetBook);

                if (result == true)
                {
                    Clear_Btn_Click(sender, e);
                    BindBookToBookDetails(targetBook);
                }
            }
            else if (_requestType == "insert")
            {
                bool result = _bookService.InsertNewBook(targetBook);

                if (result == true)
                {
                    Clear_Btn_Click(sender, e);

                    BindBookToBookDetails(targetBook);
                }
            }
            else
            {
                MessageBox.Show("Something went wrong, try again.");
            }
        }

        private void AddBook_Btn_Click(object sender, RoutedEventArgs e)
        {
            _requestType = "insert";

            ClearBookDetails();
            EnableBookDetails();
            AddBook_Btn.IsEnabled = false;
            Edit_Btn.IsEnabled = false;
            Delete_Btn.IsEnabled = false;
            Cancel_Btn.IsEnabled = true;
            Save_Btn.IsEnabled = true;
        }
    }
}