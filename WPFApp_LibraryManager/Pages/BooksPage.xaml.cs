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
using System.Linq;
using System.Windows.Media.Imaging;
using WPFApp_LibraryManager.Utils;

namespace WPFApp_LibraryManager.Pages
{
    /// <summary>
    /// Interaction logic for BooksPage.xaml
    /// </summary>
    public partial class BooksPage : UserControl
    {
        private AuthorService _authorService;

        public BooksPage()
        {
            InitializeComponent();
            _authorService = new AuthorService();
            //TestSqlQuery();
            GetBooksPageData();
            BindAuthors();
            BindPublishers();
        }

        public void BindAuthors()
        {

            var authorList = _authorService.GetAuthors();

            AuthorFilter_Cbo.ItemsSource = authorList;
            AuthorFilter_Cbo.DisplayMemberPath = "FullName";
            AuthorFilter_Cbo.SelectedValuePath = "Id";
            AuthorFilter_Cbo.SelectedIndex = 0;

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
            string query = SqlQueries.BookDetailsQuery;

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, DbContext.GetSqlConnection());

            using (sqlDataAdapter)
            {
                DataTable booksTable = new DataTable();
                sqlDataAdapter.Fill(booksTable);

                var bookList = new List<BookDetails>();

                foreach (DataRow bookRow in booksTable.Rows)
                {
                    BookDetails bookDetail = new BookDetails();

                    bookDetail.BookId = (int)bookRow["BookId"];
                    bookDetail.Title = (string)bookRow["Title"];
                    bookDetail.ISBN = (string)bookRow["ISBN"];
                    bookDetail.AuthorId = (int)bookRow["AuthorId"];
                    bookDetail.AuthorName = (string)bookRow["AuthorFullName"];
                    bookDetail.PublisherId = (int)bookRow["PublisherId"];
                    bookDetail.PublisherName = (string)bookRow["PublisherName"];
                    bookDetail.CategoryId = (int)bookRow["CategoryId"];
                    bookDetail.CategoryName = (string)bookRow["CategoryName"];
                    bookDetail.PublishedYear = (int)bookRow["PublishedYear"];
                    bookDetail.CoverURL = (string)bookRow["CoverURL"];

                    bookList.Add(bookDetail);

                }

                BookList_Dtg.ItemsSource = bookList;
            }
        }

        private void Clear_Btn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ClearFilters();
            ClearSearch();
            ClearBookDetails();
            Edit_Btn.IsEnabled = false;
            Delete_Btn.IsEnabled = false;
        }

        private void BookList_Dtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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

                Edit_Btn.IsEnabled = true;
                Delete_Btn.IsEnabled = true;
            }
        }

        private void AuthorFilter_Cbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            string query = @"SELECT 
	                            b.Id, 
	                            ISBN, 
	                            Title, 
	                            Author_Id, 
	                            CONCAT_WS(' ', [FirstName], [MiddleName], [LastName]) AS AuthorFullName,  
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

            if (Convert.ToInt32(AuthorFilter_Cbo.SelectedIndex) != 0)
            {
                query = query + " WHERE a.Id = @AuthorId";
            }

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
                    bookDetail.AuthorName = (string)bookRow["AuthorFullName"];
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

        public string GetBaseSqlSelectQuery(Enum queryMode)
        {
            string query = string.Empty;

            switch (queryMode)
            {
                case QueryMode.All:
                    query = SqlQueries.BookDetailsQuery;
                    break;
                case QueryMode.Authors:
                    query = SqlQueries.AuthorsQuery;
                    break;
                case QueryMode.Books:
                    //code books
                    break;
                case QueryMode.Categories:
                    query = SqlQueries.CategoriesQuery;
                    break;
                case QueryMode.Publishers:
                    query = SqlQueries.PublishersQuery;
                    break;
            }

            return query;
        }

        public string GetSqlWhereClauseForSearchLocations(List<Enum> querySearchLocations, string searchString)
        {
            string query = string.Empty;

            if (!querySearchLocations.Contains(QuerySearchLocation.None))
            {
                foreach (Enum queryLocation in querySearchLocations)
                {
                    switch (queryLocation)
                    {
                        case QuerySearchLocation.Authors:
                            query = query + "FirstName LIKE '%@SearchString%' OR MiddleName LIKE '%@SearchString%' OR LastName LIKE '%@SearchString%'";
                            break;
                        case QuerySearchLocation.Titles:
                            query = query + "Title LIKE '%@SearchString%'";
                            break;
                        case QuerySearchLocation.Categories:
                            query = query + "CategoryName LIKE '%@SearchString%'";
                            break;
                        case QuerySearchLocation.ISBNs:
                            query = query + "ISBN LIKE '%@SearchString%'";
                            break;
                        case QuerySearchLocation.Publishers:
                            query = query + "PublisherName LIKE '%@SearchString%'";
                            break;
                    }

                    if (!(queryLocation == querySearchLocations.Last()))
                    {
                        query = query + " OR ";
                    }
                }
            }

            return query;
        }

        public string GetSqlWhereClauseForFilters(List<Enum> queryFilters)
        {
            string query = string.Empty;

            if (!queryFilters.Contains(QueryFilter.None))
            {
                foreach (Enum queryFilter in queryFilters)
                {
                    switch (queryFilter)
                    {
                        case QueryFilter.Authors:
                            query = query + "AuthorId = @AuthorId";
                            break;
                        case QueryFilter.Categories:
                            query = query + "CategoryId = @CategoryId";
                            break;
                        case QueryFilter.Publishers:
                            query = query + "PublisherId = @PublisherId";
                            break;
                    }

                    if (!(queryFilter == queryFilters.Last()))
                    {
                        query = query + " AND ";
                    }
                }
            }

            return query;
        }

        public string GetSqlWhereClause(List<Enum> querySearchLocations, string searchString, List<Enum> queryFilters)
        {
            string query = string.Empty;

            if (!(queryFilters.Contains(QueryFilter.None)) && !(querySearchLocations.Contains(QuerySearchLocation.None)))
            {
                query = query + " WHERE (" + GetSqlWhereClauseForFilters(queryFilters) + ") AND (" + GetSqlWhereClauseForSearchLocations(querySearchLocations, searchString) + ")";
            }
            else if (!queryFilters.Contains(QueryFilter.None))
            {
                query = query + " WHERE " + GetSqlWhereClauseForFilters(queryFilters);
            }
            else
            {
                query = query + " WHERE " + GetSqlWhereClauseForSearchLocations(querySearchLocations, searchString);
            }

            return query;
        }

        public string GenerateSqlQuery(Enum queryMode, List<Enum> querySearchLocations, string searchString, List<Enum> queryFilters)
        {
            string query = string.Empty;

            query = GetBaseSqlSelectQuery(queryMode);
            query = query + GetSqlWhereClause(querySearchLocations, searchString, queryFilters);

            return query;
        }

        public void TestSqlQuery()
        {
            List<Enum> querySearchLocations = new List<Enum>();
            //querySearchLocations.Add(QuerySearchLocation.None);
            querySearchLocations.Add(QuerySearchLocation.Authors);
            querySearchLocations.Add(QuerySearchLocation.ISBNs);
            querySearchLocations.Add(QuerySearchLocation.Titles);
            querySearchLocations.Add(QuerySearchLocation.Categories);
            querySearchLocations.Add(QuerySearchLocation.Publishers);

            List<Enum> queryFilters = new List<Enum>();
            //queryFilters.Add(QueryFilter.None);
            queryFilters.Add(QueryFilter.Authors);
            queryFilters.Add(QueryFilter.Categories);
            queryFilters.Add(QueryFilter.Publishers);

            //SqlTest.Text = GenerateSqlQuery(QueryMode.All, querySearchLocations,string.Empty, queryFilters);

        }

        private void Edit_Btn_Click(object sender, RoutedEventArgs e)
        {
            EnableBookDetails();
        }

        private void EnableBookDetails()
        {
            SelectedBook_Title_Txt.IsEnabled = true;
            SelectedBook_ISBN_Txt.IsEnabled = true;
            SelectedBook_Author_Cbo.IsEnabled = true;
            SelectedBook_Category_Cbo.IsEnabled = true;
            SelectedBook_Publisher_Cbo.IsEnabled = true;
            SelectedBook_Published_Year_Txt.IsEnabled = true;
        }
        private void DisableBookDetails()
        {
            SelectedBook_Title_Txt.IsEnabled = true;
            SelectedBook_ISBN_Txt.IsEnabled = true;
            SelectedBook_Author_Cbo.IsEnabled = true;
            SelectedBook_Category_Cbo.IsEnabled = true;
            SelectedBook_Publisher_Cbo.IsEnabled = true;
            SelectedBook_Published_Year_Txt.IsEnabled = true;
        }

        private void ClearBookDetails()
        {
            SelectedBook_Title_Txt.Text = string.Empty;
            SelectedBook_ISBN_Txt.Text = string.Empty;
            SelectedBook_Author_Cbo.Text = string.Empty;
            SelectedBook_Category_Cbo.Text = string.Empty;
            SelectedBook_Publisher_Cbo.Text = string.Empty;
            SelectedBook_Published_Year_Txt.Text = string.Empty;
            SelectedBook_Cover_img.Source = new BitmapImage(new Uri(@"../Images/CoverPlaceholder.png", UriKind.Relative));
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
    }
}
