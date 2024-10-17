using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFApp_LibraryManager.Pages;
using WPFApp_LibraryManager.Components;

namespace WPFApp_LibraryManager.Components
{
    /// <summary>
    /// Interaction logic for NavigationBar.xaml
    /// </summary>
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
