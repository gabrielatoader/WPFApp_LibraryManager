using System.Windows;
using WPFApp_LibraryManager.Components;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Pages;

namespace WPFApp_LibraryManager
{
    public partial class MainWindow : Window
    {
        public MainWindow(IAuthorService authorService, IBookService bookService, ICategoryService categoryService, IPublisherService publisherService)
        {
            InitializeComponent();

            NavigationBar_CC.Content = new NavigationBar(authorService, bookService, categoryService, publisherService);
            Content_CC.Content = new BooksPage(authorService, bookService, categoryService, publisherService);
        }
    }
}