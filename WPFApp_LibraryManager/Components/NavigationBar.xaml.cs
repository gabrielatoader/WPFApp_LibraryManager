using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Pages;

namespace WPFApp_LibraryManager.Components
{
    public partial class NavigationBar : UserControl
    {
        private IAuthorService _authorService;

        private IBookService _bookService;

        private ICategoryService _categoryService;

        private IPublisherService _publisherService;

        public NavigationBar(IAuthorService authorService, IBookService bookService, ICategoryService categoryService, IPublisherService publisherService)
        {
            _authorService = authorService;
            _publisherService = publisherService;
            _bookService = bookService;
            _categoryService = categoryService;

            InitializeComponent();

            BooksPage_Btn.IsEnabled = false;
        }

        private void Exit_Btn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BooksPage_Btn_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow((DependencyObject)sender);

            ContentControl contentControl = new ContentControl();
            contentControl = (ContentControl)parentWindow.FindName("Content_CC");

            contentControl.Content = new BooksPage(_authorService, _bookService, _categoryService, _publisherService);

            BooksPage_Btn.IsEnabled = false;
            AuthorsPage_Btn.IsEnabled = true;
            CategoriesPage_Btn.IsEnabled = true;
            HomePage_Btn.IsEnabled = true;
            PublishersPage_Btn.IsEnabled = true;
        }

        private void AuthorsPage_Btn_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow((DependencyObject)sender);

            ContentControl contentControl = new ContentControl();
            contentControl = (ContentControl)parentWindow.FindName("Content_CC");

            contentControl.Content = new AuthorsPage(_authorService);

            AuthorsPage_Btn.IsEnabled = false;
            BooksPage_Btn.IsEnabled = true;
            CategoriesPage_Btn.IsEnabled = true;
            HomePage_Btn.IsEnabled = true;
            PublishersPage_Btn.IsEnabled = true;
        }

        private void CategoriesPage_Btn_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow((DependencyObject)sender);

            ContentControl contentControl = new ContentControl();
            contentControl = (ContentControl)parentWindow.FindName("Content_CC");

            contentControl.Content = new CategoriesPage(_categoryService);

            CategoriesPage_Btn.IsEnabled = false;
            AuthorsPage_Btn.IsEnabled = true;
            BooksPage_Btn.IsEnabled = true;
            HomePage_Btn.IsEnabled = true;
            PublishersPage_Btn.IsEnabled = true;
        }

        private void PublishersPage_Btn_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow((DependencyObject)sender);

            ContentControl contentControl = new ContentControl();
            contentControl = (ContentControl)parentWindow.FindName("Content_CC");

            contentControl.Content = new PublishersPage(_publisherService);

            PublishersPage_Btn.IsEnabled = false;
            AuthorsPage_Btn.IsEnabled = true;
            BooksPage_Btn.IsEnabled = true;
            CategoriesPage_Btn.IsEnabled = true;
            HomePage_Btn.IsEnabled = true;
        }

        private void HomePage_Btn_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow((DependencyObject)sender);

            ContentControl contentControl = new ContentControl();
            contentControl = (ContentControl)parentWindow.FindName("Content_CC");

            contentControl.Content = new HomePage();

            HomePage_Btn.IsEnabled = false;
            AuthorsPage_Btn.IsEnabled = true;
            BooksPage_Btn.IsEnabled = true;
            CategoriesPage_Btn.IsEnabled = true;
            PublishersPage_Btn.IsEnabled = true;
        }
    }
}