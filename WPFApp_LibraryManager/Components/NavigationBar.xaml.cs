using System.Windows;
using System.Windows.Controls;
using WPFApp_LibraryManager.Pages;

namespace WPFApp_LibraryManager.Components
{
    public partial class NavigationBar : UserControl
    {
        public NavigationBar()
        {
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
            contentControl.Content = new BooksPage();
        }

        private void Authors_Btn_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow((DependencyObject)sender);
            ContentControl contentControl = new ContentControl();
            contentControl = (ContentControl)parentWindow.FindName("Content_CC");
            contentControl.Content = new AuthorsPage();
        }

        private void Publishers_Btn_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow((DependencyObject)sender);
            ContentControl contentControl = new ContentControl();
            contentControl = (ContentControl)parentWindow.FindName("Content_CC");
            contentControl.Content = new PublishersPage();
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