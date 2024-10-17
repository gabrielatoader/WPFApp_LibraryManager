using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using WPFApp_LibraryManager.Components;
using WPFApp_LibraryManager.Views;

namespace WPFApp_LibraryManager
{
   

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Navigation_CC.Content = new NavigationBar();
            Content_CC.Content = new BooksView();
        }
    }
}
