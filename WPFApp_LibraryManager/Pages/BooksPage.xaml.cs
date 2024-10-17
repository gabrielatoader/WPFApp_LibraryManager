using System.Data.SqlClient;
using System.Data;
using System.Windows.Controls;
using System.Configuration;
using WPFApp_LibraryManager.Components;
using WPFApp_LibraryManager.Services;
using System.Windows;
using System.Collections.Generic;
using WPFApp_LibraryManager.Models;
using System;
using System.Windows.Documents;
using System.Windows.Media;

namespace WPFApp_LibraryManager.Pages
{
    /// <summary>
    /// Interaction logic for BooksPage.xaml
    /// </summary>
    public partial class BooksPage : UserControl
    {
        public BooksPage()
        {
            InitializeComponent();
            GetBooksPageData();
            BindAuthors();
            BindPublishers();
        }

        public void BindAuthors()
        {
            string query = @"SELECT Id, CONCAT_WS(' ', [FirstName], [MiddleName], [LastName]) AS Name FROM Authors";
            
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, DbContext.GetSqlConnection());
            
            using (sqlDataAdapter)
            {
                DataTable authorsTable = new DataTable();
                
                sqlDataAdapter.Fill(authorsTable);

                List<Author> authorList = new List<Author>();

                Author header = new Author();
                header.Id = 0;
                header.Name = "- AUTHOR -";
                authorList.Add(header);

                foreach (DataRow authorRow in authorsTable.Rows)
                {
                    Author author = new Author();

                    author.Id = (int)authorRow["Id"];
                    author.Name = (string)authorRow["Name"];

                    authorList.Add(author);

                }

                AuthorFilter_Cbo.ItemsSource = authorList;
                AuthorFilter_Cbo.DisplayMemberPath = "Name";
                AuthorFilter_Cbo.SelectedValuePath = "Id";
                AuthorFilter_Cbo.SelectedIndex = 0;
            }
        }

        public void BindPublishers()
        {
            string query = @"SELECT Id, Name FROM Publishers";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, DbContext.GetSqlConnection());
            using (sqlDataAdapter)
            {
                DataTable publishersTable = new DataTable();
                sqlDataAdapter.Fill(publishersTable);

                List<Publisher> publisherList = new List<Publisher>();

                Publisher header = new Publisher();
                header.Id = -1;
                header.Name = "- PUBLISHER -";
                publisherList.Add(header);

                foreach (DataRow publisherRow in publishersTable.Rows)
                {
                    Publisher publisher = new Publisher();

                    publisher.Id = (int)publisherRow["Id"];
                    publisher.Name = (string)publisherRow["Name"];

                    publisherList.Add(publisher);

                }

                PublisherFilter_Cbo.ItemsSource = publisherList;
                PublisherFilter_Cbo.DisplayMemberPath = "Name";
                PublisherFilter_Cbo.SelectedValuePath = "Id";
                PublisherFilter_Cbo.SelectedIndex = 0;
            }
        }

        public void GetBooksPageData()
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
	                            Category_Id, 
	                            c.Name AS Category, 
	                            Cover_URL
                            FROM 
	                            Books b 
	                            JOIN Publishers p 
		                            ON b.Publisher_Id = p.Id 
	                            JOIN Authors a 
		                            ON b.Author_Id = a.Id
	                            JOIN Category c
		                            ON b.Category_Id = c.Id";

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, DbContext.GetSqlConnection());

            using (sqlDataAdapter)
            {
                DataTable booksTable = new DataTable();
                sqlDataAdapter.Fill(booksTable);

                var bookList = new List<BookDetails>();

                foreach (DataRow bookRow in booksTable.Rows)
                {
                    BookDetails bookDetail = new BookDetails();

                    bookDetail.BookId = (int)bookRow["Id"];
                    bookDetail.Title = (string)bookRow["Title"];
                    bookDetail.ISBN = (string)bookRow["ISBN"];
                    bookDetail.AuthorId = (int)bookRow["Author_Id"];
                    bookDetail.AuthorName = (string)bookRow["Author"];
                    bookDetail.PublisherId = (int)bookRow["Publisher_Id"];
                    bookDetail.PublisherName = (string)bookRow["Publisher"];
                    bookDetail.CategoryId = (int)bookRow["Category_Id"];
                    bookDetail.CategoryName = (string)bookRow["Category"];
                    bookDetail.PublishedYear = (int)bookRow["Year"];
                    bookDetail.CoverURL = (string)bookRow["Cover_URL"];

                    bookList.Add(bookDetail);

                }

                BookList_Dtg.ItemsSource = bookList;
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
            AuthorFilter_Cbo.SelectedIndex = 0;
            PublisherFilter_Cbo.SelectedIndex = 0;
            CategoryFilter_Cbo.SelectedIndex = 0;
            SelectedBook_Title_Txt.Text = string.Empty;
            SelectedBook_ISBN_Txt.Text = string.Empty;
            SelectedBook_Author_Cbo.Text = string.Empty;
            SelectedBook_Category_Cbo.Text = string.Empty;
            SelectedBook_Publisher_Cbo.Text = string.Empty;
            SelectedBook_Published_Year_Txt.Text = string.Empty;
            Instructions_Lbl.Visibility = Visibility.Visible;
            BookDetails_Grd.Visibility = Visibility.Hidden;

            ImageSourceConverter coverConverter = new ImageSourceConverter();
            SelectedBook_Cover_img.Source = (ImageSource)coverConverter.ConvertFromString("https://dzinejs.lv/wp-content/plugins/lightbox/images/No-image-found.jpg");
        }

        private void BookList_Dtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Instructions_Lbl.Visibility = Visibility.Hidden;
            BookDetails_Grd.Visibility = Visibility.Visible;

            DataGrid dataGrid = (DataGrid)sender;
            var selectedRow = dataGrid.SelectedItem as BookDetails;
            if (selectedRow != null)
            {
                SelectedBook_Title_Txt.Text = selectedRow.Title;
                SelectedBook_ISBN_Txt.Text = selectedRow.ISBN;
                SelectedBook_Author_Cbo.Text = selectedRow.AuthorName;
                SelectedBook_Category_Cbo.Text = selectedRow.CategoryName;
                SelectedBook_Publisher_Cbo.Text = selectedRow.PublisherName;
                SelectedBook_Published_Year_Txt.Text = selectedRow.PublishedYear.ToString();

                ImageSourceConverter coverConverter = new ImageSourceConverter();

                SelectedBook_Cover_img.Source = (ImageSource)coverConverter.ConvertFromString(selectedRow.CoverURL);
            }
        }

        private void AuthorFilter_Cbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Convert.ToInt32(AuthorFilter_Cbo.SelectedIndex) != 0)
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
	                            Category_Id, 
	                            c.Name AS Category, 
	                            Cover_URL
                            FROM 
	                            Books b 
	                            JOIN Publishers p 
		                            ON b.Publisher_Id = p.Id 
	                            JOIN Authors a 
		                            ON b.Author_Id = a.Id
	                            JOIN Category c
		                            ON b.Category_Id = c.Id
                            WHERE a.Id = @AuthorId";
                SqlCommand cmd = new SqlCommand(query, DbContext.GetSqlConnection());
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@AuthorId", Convert.ToInt32(AuthorFilter_Cbo.SelectedIndex));

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                using (sqlDataAdapter)
                {
                    DataTable booksTable = new DataTable();
                    sqlDataAdapter.Fill(booksTable);

                    var bookList = new List<BookDetails>();

                    foreach (DataRow bookRow in booksTable.Rows)
                    {
                        BookDetails bookDetail = new BookDetails();

                        bookDetail.BookId = (int)bookRow["Id"];
                        bookDetail.Title = (string)bookRow["Title"];
                        bookDetail.ISBN = (string)bookRow["ISBN"];
                        bookDetail.AuthorId = (int)bookRow["Author_Id"];
                        bookDetail.AuthorName = (string)bookRow["Author"];
                        bookDetail.PublisherId = (int)bookRow["Publisher_Id"];
                        bookDetail.PublisherName = (string)bookRow["Publisher"];
                        bookDetail.CategoryId = (int)bookRow["Category_Id"];
                        bookDetail.CategoryName = (string)bookRow["Category"];
                        bookDetail.PublishedYear = (int)bookRow["Year"];
                        bookDetail.CoverURL = (string)bookRow["Cover_URL"];

                        bookList.Add(bookDetail);

                    }

                    BookList_Dtg.ItemsSource = bookList;
                }

            }
        }
    }
}
