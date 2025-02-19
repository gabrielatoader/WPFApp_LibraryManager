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
            
            BindBookListToGrid(_bookService.GetBookList());

            // bind data to search area
            BindAuthorListToCbo(AuthorFilter_Cbo, 0);
            BindPublisherListToCbo(PublisherFilter_Cbo, 0);
            BindCategoryListToCbo(CategoryFilter_Cbo, 0);
            
            // bind data to book detail area
            BindAuthorListToCbo(TargetBook_Author_Cbo, 0);
            BindCategoryListToCbo(TargetBook_Category_Cbo, 0);
            BindPublisherListToCbo(TargetBook_Publisher_Cbo, 0);

            DisableSearchButton();
            DisableClearButton();
        }

        public void BindAuthorListToCbo(ComboBox cbo, int selectedIndex)
        {
            List<Author> authorList = _authorService.GetAuthorListWithListHeader();

            cbo.ItemsSource = authorList;
            cbo.DisplayMemberPath = "FullName";
            cbo.SelectedValuePath = "Id";
            cbo.SelectedIndex = selectedIndex;
        }

        public void BindPublisherListToCbo(ComboBox cbo, int selectedIndex)
        {
            List<Publisher> publisherList = _publisherService.GetPublisherListWithListHeader();

            cbo.ItemsSource = publisherList;
            cbo.DisplayMemberPath = "Name";
            cbo.SelectedValuePath = "Id";
            cbo.SelectedIndex = selectedIndex;
        }

        public void BindCategoryListToCbo(ComboBox cbo, int selectedIndex)
        {
            List<Category> categoryList = _categoryService.GetCategoryListWithListHeader();

            cbo.ItemsSource = categoryList;
            cbo.DisplayMemberPath = "Name";
            cbo.SelectedValuePath = "Id";
            cbo.SelectedIndex = selectedIndex;
        }

        public void BindBookListToGrid(List<Book> bookList)
        {
            BookList_Dtg.ItemsSource = bookList;
        }
        
        private void BindBookToBookDetails(Book book)
        {
            TargetBook_Title_Txt.Text = book.Title;
            TargetBook_ISBN_Txt.Text = book.Isbn;
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
                TargetBook_Cover_img.Source = new BitmapImage(new Uri(@"../Images/BookCoverPlaceholder.png", UriKind.RelativeOrAbsolute));
            }

            DisableBookDetails();
        }
        
        private void Clear_Btn_Click(object sender, RoutedEventArgs e)
        {
            _requestType = "";

            ClearSearch();
            ClearBookGrid();
            ClearBookDetails();
            DisableSearchButton();
            DisableClearButton();
        }
        
        private void BookList_Dtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;

            var selectedRow = dataGrid.SelectedItem as Book;

            if (selectedRow != null)
            {
                BindBookToBookDetails(selectedRow);

                EnableClearButton();
            }
        }

        private void Edit_Btn_Click(object sender, RoutedEventArgs e)
        {
            _requestType = "update";

            EnableBookDetails();
        }

        private void EnableBookDetails()
        {
            TargetBook_Title_Txt.IsEnabled= true;
            TargetBook_ISBN_Txt.IsEnabled= true;
            TargetBook_Author_Cbo.IsEnabled= true;
            TargetBook_Category_Cbo.IsEnabled= true;
            TargetBook_Publisher_Cbo.IsEnabled= true;
            TargetBook_PublishedYear_Txt.IsEnabled= true;
            TargetBook_CoverURL_Txt.IsEnabled= true;
            TargetBook_Cover_img.IsEnabled = true;

            EnableSaveCancelButtons();
            DisableEditDeleteButtons();
        }

        private void DisableBookDetails()
        {
            TargetBook_Title_Txt.IsEnabled= false;
            TargetBook_ISBN_Txt.IsEnabled= false;
            TargetBook_Author_Cbo.IsEnabled= false;
            TargetBook_Category_Cbo.IsEnabled= false;
            TargetBook_Publisher_Cbo.IsEnabled= false;
            TargetBook_PublishedYear_Txt.IsEnabled= false;
            TargetBook_CoverURL_Txt.IsEnabled= false;
            TargetBook_Cover_img.IsEnabled = false;

            EnableEditDeleteButtons();
            DisableSaveCancelButtons();
        }

        private void ClearBookDetails()
        {
            TargetBook_Title_Txt.Text = string.Empty;
            TargetBook_ISBN_Txt.Text = string.Empty;
            TargetBook_Author_Cbo.SelectedIndex = 0;
            TargetBook_Category_Cbo.SelectedIndex = 0;
            TargetBook_Publisher_Cbo.SelectedIndex = 0;
            TargetBook_PublishedYear_Txt.Text = string.Empty;
            TargetBook_CoverURL_Txt.Text = string.Empty;
            TargetBook_Cover_img.Source = new BitmapImage(new Uri(@"../Images/BookCoverPlaceholder.png", UriKind.Relative));

            DisableBookDetails();
            DisableEditDeleteButtons();
        }
        private void ClearSearch()
        {
            AuthorFilter_Cbo.SelectedIndex = 0;
            PublisherFilter_Cbo.SelectedIndex = 0;
            CategoryFilter_Cbo.SelectedIndex = 0;
            Title_Chk.IsChecked = false;
            Author_Chk.IsChecked = false;
            Publisher_Chk.IsChecked = false;
            ISBN_Chk.IsChecked = false;
            Category_Chk.IsChecked = false;
            Search_Txt.Text = string.Empty;
        }

        private void ClearBookGrid()
        {
            BindBookListToGrid(_bookService.GetBookList());
        }

        private void Cancel_Btn_Click(object sender, RoutedEventArgs e)
        {
            _requestType = "";

            if ((Book)BookList_Dtg.SelectedItem != null)
            {
                BindBookToBookDetails((Book)BookList_Dtg.SelectedItem);
            }
            else
            {
                ClearBookDetails();
            }
        }

        private void Delete_Btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Book activeBook = (Book)BookList_Dtg.SelectedItem;

                _bookService.DeleteBook(activeBook.Id);

                Clear_Btn_Click(sender, e);
            }
        }

        private void Save_Btn_Click(object sender, RoutedEventArgs e)
        {
            Book targetBook = new Book();
            targetBook.Title = TargetBook_Title_Txt.Text;
            targetBook.Isbn = TargetBook_ISBN_Txt.Text;
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

            bool result = false;

            if (_requestType == "update")
            {
                Book activeBook = (Book)BookList_Dtg.SelectedItem;

                targetBook.Id = activeBook.Id;

                result = _bookService.UpdateBook(targetBook);
            }
            else if (_requestType == "insert")
            {
                result = _bookService.InsertBook(targetBook);
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

        private void AddBook_Btn_Click(object sender, RoutedEventArgs e)
        {
            _requestType = "insert";

            ClearBookDetails();

            EnableBookDetails();
        }
        
        private void Search_Btn_Click(object sender, RoutedEventArgs e)
        {
            BookFilters bookFilters = new BookFilters()
            {
                AuthorId = Convert.ToInt32(AuthorFilter_Cbo.SelectedValue),
                CategoryId = Convert.ToInt32(CategoryFilter_Cbo.SelectedValue),
                PublisherId = Convert.ToInt32(PublisherFilter_Cbo.SelectedValue),
                SearchString = Search_Txt.Text,
                SearchInTitle = (bool)Title_Chk.IsChecked,
                SearchInAuthor = (bool)Author_Chk.IsChecked,
                SearchInPublisher = (bool)Publisher_Chk.IsChecked,
                SearchInISBN = (bool)ISBN_Chk.IsChecked,
                SearchInCategory = (bool)Category_Chk.IsChecked
            };
            
            if (String.IsNullOrEmpty(bookFilters.SearchString) && bookFilters.AuthorId == 0 && bookFilters.CategoryId == 0 && bookFilters.PublisherId == 0)
            {
                MessageBox.Show("Search and filter options are empty, please choose one or more to start.");
            }
            else if (!String.IsNullOrEmpty(bookFilters.SearchString) && bookFilters.SearchLocationIsUndefined)
            {
                MessageBox.Show("Please select at least one checkbox for the search location.");
            }
            else if (String.IsNullOrEmpty(bookFilters.SearchString) && !bookFilters.SearchLocationIsUndefined)
            {
                MessageBox.Show("Search box is empty but search location is checked, please add a search string or remove the search location.");
            }
            else 
            {
                List<Book> bookList = _bookService.GetFilteredBookList(bookFilters);

                if (bookList == null || bookList.Count == 0)
                {
                    MessageBox.Show("Could not find books to match search request.");
                }
                else
                {
                    BindBookListToGrid(bookList);
                }
            }
        }

        private void PublisherFilter_Cbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableSearchButton();
            EnableClearButton();
        }

        private void AuthorFilter_Cbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableSearchButton();
            EnableClearButton();
        }

        private void CategoryFilter_Cbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnableSearchButton();
            EnableClearButton();
        }

        private void Search_Txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableSearchButton();
            EnableClearButton();
        }

        private void EnableSaveCancelButtons()
        {
            Cancel_Btn.IsEnabled = true;
            Save_Btn.IsEnabled = true;

            Style accentButtonStyle = TryFindResource("MahApps.Styles.Button.Square.Accent") as Style;

            Cancel_Btn.Style = accentButtonStyle;
            Save_Btn.Style = accentButtonStyle;
        }

        private void DisableSaveCancelButtons()
        {
            Cancel_Btn.IsEnabled = false;
            Save_Btn.IsEnabled = false;

            Style normalButtonStyle = TryFindResource("MahApps.Styles.Button.Square") as Style;

            Cancel_Btn.Style = normalButtonStyle;
            Save_Btn.Style = normalButtonStyle;
        }

        private void EnableEditDeleteButtons()
        {
            Edit_Btn.IsEnabled = true;
            Delete_Btn.IsEnabled = true;
            
            Style accentButtonStyle = TryFindResource("MahApps.Styles.Button.Square.Accent") as Style;

            Edit_Btn.Style = accentButtonStyle;
            Delete_Btn.Style = accentButtonStyle;
        }

        private void DisableEditDeleteButtons()
        {
            Edit_Btn.IsEnabled = false;
            Delete_Btn.IsEnabled = false;

            Style normalButtonStyle = TryFindResource("MahApps.Styles.Button.Square") as Style;
            
            Edit_Btn.Style = normalButtonStyle;
            Delete_Btn.Style = normalButtonStyle;
        }

        private void EnableSearchButton()
        {
            Search_Btn.IsEnabled = true;
            
            Style accentButtonStyle = TryFindResource("MahApps.Styles.Button.Square.Accent") as Style;

            Search_Btn.Style = accentButtonStyle;
        }

        private void DisableSearchButton()
        {
            Search_Btn.IsEnabled = false;
            
            Style normalButtonStyle = TryFindResource("MahApps.Styles.Button.Square") as Style;

            Search_Btn.Style = normalButtonStyle;
        }
        
        private void EnableClearButton()
        {
            Clear_Btn.IsEnabled = true;

            Style accentButtonStyle = TryFindResource("MahApps.Styles.Button.Square.Accent") as Style;

            Clear_Btn.Style = accentButtonStyle;
        }

        private void DisableClearButton()
        {
            Clear_Btn.IsEnabled = false;

            Style normalButtonStyle = TryFindResource("MahApps.Styles.Button.Square") as Style;

            Clear_Btn.Style = normalButtonStyle;
        }
    }
}