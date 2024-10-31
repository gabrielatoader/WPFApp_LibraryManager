using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;
using WPFApp_LibraryManager.Utils;

namespace WPFApp_LibraryManager.Pages
{
    public partial class BooksPage : UserControl
    {
        private IAuthorService _authorService;
        private IPublisherService _publisherService;
        private IBookService _bookService;
        private ICategoryService _categoryService;
        private string _requestType = "unknown";

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
            _requestType = "unknown";

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
                TargetBook_Title_Txt.Text = selectedRow.Title;
                TargetBook_ISBN_Txt.Text = selectedRow.ISBN;
                TargetBook_PublishedYear_Txt.Text = selectedRow.PublishedYear.ToString();
                TargetBook_CoverURL_Txt.Text = selectedRow.CoverURL;
                BindAuthorsToCbo(TargetBook_Author_Cbo, selectedRow.AuthorId);
                BindCategoriesToCbo(TargetBook_Category_Cbo, selectedRow.CategoryId);
                BindPublishersToCbo(TargetBook_Publisher_Cbo, selectedRow.PublisherId);
                try
                {
                    ImageSourceConverter coverConverter = new ImageSourceConverter();
                    TargetBook_Cover_img.Source = (ImageSource)coverConverter.ConvertFromString(selectedRow.CoverURL);
                }
                catch(Exception ex)
                {
                    TargetBook_Cover_img.Source = new BitmapImage(new Uri(@"../Images/CoverPlaceholder.png", UriKind.Relative));
                }

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
            _requestType = "unknown";

            DisableBookDetails();
            Cancel_Btn.IsEnabled = false;
            Save_Btn.IsEnabled = false;
            Delete_Btn.IsEnabled = true;
            Edit_Btn.IsEnabled = true;
            AddBook_Btn.IsEnabled = true;
        }

        private void Delete_Btn_Click(object sender, RoutedEventArgs e)
        {
            //ToDo: delete from table(s)
        }

        private void Save_Btn_Click(object sender, RoutedEventArgs e)
        {
            Book activeBook = (Book)BookList_Dtg.SelectedItem;
            Book targetBook = new Book();
            targetBook.BookId = activeBook.BookId;
            targetBook.Title = TargetBook_Title_Txt.Text;
            targetBook.ISBN = TargetBook_ISBN_Txt.Text;
            targetBook.AuthorName = TargetBook_Author_Cbo.Text;
            targetBook.AuthorId = TargetBook_Author_Cbo.SelectedIndex;
            targetBook.CategoryName = TargetBook_Category_Cbo.Text;
            targetBook.CategoryId = TargetBook_Category_Cbo.SelectedIndex;
            targetBook.PublisherName = TargetBook_Publisher_Cbo.Text;
            targetBook.PublisherId = TargetBook_Publisher_Cbo.SelectedIndex;
            targetBook.PublishedYear = Convert.ToInt32(TargetBook_PublishedYear_Txt.Text);
            targetBook.CoverURL = TargetBook_CoverURL_Txt.Text;

            BookValidator bookValidator = new BookValidator();

            if (bookValidator.isValidBook(targetBook))
            {
                if (_requestType == "update")
                {
                    _bookService.UpdateBook(targetBook);

                    MessageBox.Show("Book updated successfully!");
                }
                else if (_requestType == "insert")
                {
                    _bookService.InsertNewBook(targetBook);

                    MessageBox.Show("Book added successfully!");
                }
                else
                {
                    MessageBox.Show("Unknown request");
                }

                DisableBookDetails();
                Cancel_Btn.IsEnabled = false;
                Save_Btn.IsEnabled = false;
                Delete_Btn.IsEnabled = true;
                Edit_Btn.IsEnabled = true;
                AddBook_Btn.IsEnabled = true;
                ClearDataGrid();
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