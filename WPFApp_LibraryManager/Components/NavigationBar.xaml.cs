using System.Windows;
using System.Windows.Controls;
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
        }

        private void Exit_Btn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Books_Btn_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow((DependencyObject)sender);
            ContentControl contentControl = new ContentControl();
            contentControl = (ContentControl)parentWindow.FindName("Content_CC");
            contentControl.Content = new BooksPage(_authorService, _bookService, _categoryService, _publisherService);
        }

        private void Authors_Btn_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow((DependencyObject)sender);
            ContentControl contentControl = new ContentControl();
            contentControl = (ContentControl)parentWindow.FindName("Content_CC");
            contentControl.Content = new AuthorsPage();
        }
        
        private void Categories_Btn_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow((DependencyObject)sender);
            ContentControl contentControl = new ContentControl();
            contentControl = (ContentControl)parentWindow.FindName("Content_CC");
            contentControl.Content = new CategoriesPage(_categoryService);
        }

        private void Publishers_Btn_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow((DependencyObject)sender);
            ContentControl contentControl = new ContentControl();
            contentControl = (ContentControl)parentWindow.FindName("Content_CC");
            contentControl.Content = new PublishersPage(_publisherService);
        }

        private void Home_Btn_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow((DependencyObject)sender);
            ContentControl contentControl = new ContentControl();
            contentControl = (ContentControl)parentWindow.FindName("Content_CC");
            contentControl.Content = new HomePage();
        }
    }
}