using System.Data.SqlClient;
using System.Data;
using System.Windows.Controls;
using System.Configuration;
using WPFApp_LibraryManager.Components;
using WPFApp_LibraryManager.Services;
using System.Windows;

namespace WPFApp_LibraryManager.Views
{
    /// <summary>
    /// Interaction logic for BooksView.xaml
    /// </summary>
    public partial class BooksView : UserControl
    {
        public BooksView()
        {
            InitializeComponent();
            ShowBooks();
            BookDetailArea.Content = new BooksView_Instructions();
        }

        public void ShowBooks()
        {
            string query = @"SELECT 
	                            b.Id, 
	                            ISBN, 
	                            Title, 
	                            Author_Id, 
	                            CONCAT_WS(' ', [FirstName], [MiddleName], [LastName]) AS Author,  
	                            Publisher_Id, 
	                            p.Name AS Publisher, 
	                            Published_Year as Year, 
	                            Cathegory_Id, 
	                            c.Name AS Cathegory, 
	                            Cover_URL 
                            FROM 
	                            Books b 
	                            JOIN Publishers p 
		                            ON b.Publisher_Id = p.Id 
	                            JOIN Authors a 
		                            ON b.Author_Id = a.Id
	                            JOIN Cathegory c
		                            ON b.Cathegory_Id = c.Id";

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, DbContext.GetSqlConnection());

            using (sqlDataAdapter)
            {
                DataTable booksTable = new DataTable();
                sqlDataAdapter.Fill(booksTable);
                BookList_Dtg.ItemsSource = booksTable.DefaultView;
            }
        }

        private void Clear_Btn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Title_Chk.IsChecked = false;
            Author_Chk.IsChecked = false;
            Publisher_Chk.IsChecked = false;
            ISBN_Chk.IsChecked = false;
            Category_Chk.IsChecked = false;
            Search_Txt.Text = string.Empty;
            Author_Cbo.SelectedIndex = 0;
            Publisher_Cbo.SelectedIndex = 0;
            Category_Cbo.SelectedIndex = 0;
        }

        private void BookList_Dtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BookDetailArea.Content = new BooksView_BookDetails();

            DataGrid dataGrid = (DataGrid)sender;
            DataRowView selectedRow = dataGrid.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                Search_Txt.Text = selectedRow["Title"].ToString();
            }
        }
    }
}
