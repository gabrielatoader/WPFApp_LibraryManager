using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;
using WPFApp_LibraryManager.Utils;

namespace WPFApp_LibraryManager.Repositories
{
    public class BookRepository : BaseRepository, IBookRepository
    {
        public List<Book> GetAllBooksList()
        {
            DataTable booksTable = GetResultTable(SqlQueries.AllBooksQuery);

            return ConvertBookDataTableToBooList(booksTable);
        }

        public List<Book> GetFilteredBooksByAuthor(int authorId)
        {
            DataTable booksTable = GetResultTableFilteredByAuthor(SqlQueries.BooksFilteredByAuthorQuery, authorId);

            return ConvertBookDataTableToBooList(booksTable);

        }

        public List<Book> GetFilteredBooksByPublisher(int publisherId)
        {
            DataTable booksTable = GetResultTableFilteredByPublisher(SqlQueries.BooksFilteredByPublisherQuery, publisherId);

            return ConvertBookDataTableToBooList(booksTable);

        }

        public List<Book> GetFilteredBooksByCategory(int categoryId)
        {
            DataTable booksTable = GetResultTableFilteredByCategory(SqlQueries.BooksFilteredByCategoryQuery, categoryId);

            return ConvertBookDataTableToBooList(booksTable);

        }

        public DataTable GetResultTableFilteredByAuthor(string query, int authorId)
        {
            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@AuthorId", authorId);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            using (sqlDataAdapter)
            {
                DataTable resultsTable = new DataTable();

                sqlDataAdapter.Fill(resultsTable);

                return resultsTable;
            }
        }

        public DataTable GetResultTableFilteredByPublisher(string query, int publisherId)
        {
            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@PublisherId", publisherId);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            using (sqlDataAdapter)
            {
                DataTable resultsTable = new DataTable();

                sqlDataAdapter.Fill(resultsTable);

                return resultsTable;
            }
        }

        public DataTable GetResultTableFilteredByCategory(string query, int categoryId)
        {
            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@CategoryId", categoryId);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            using (sqlDataAdapter)
            {
                DataTable resultsTable = new DataTable();

                sqlDataAdapter.Fill(resultsTable);

                return resultsTable;
            }
        }
        
        public void InsertBookInDb(string query, Book book)
        {
            _sqlConnection.Open();

            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Title", book.Title);
            cmd.Parameters.AddWithValue("@ISBN", book.ISBN);
            cmd.Parameters.AddWithValue("@AuthorId", book.AuthorId);
            cmd.Parameters.AddWithValue("@PublisherId", book.PublisherId);
            cmd.Parameters.AddWithValue("@PublishedYear", book.PublishedYear);
            cmd.Parameters.AddWithValue("@CategoryId", book.CategoryId);
            cmd.Parameters.AddWithValue("@CoverURL", book.CoverURL);

            cmd.ExecuteNonQuery();

            _sqlConnection.Close();
        }

        public void InsertNewBook(Book book)
        {
            InsertBookInDb(SqlQueries.InsertNewBookQuery, book);
        }
        
        public void UpdateBook(Book book)
        {
            UpdatebookInDb(SqlQueries.UpdateBookQuery, book);
        }
        
        public void UpdatebookInDb(string query, Book book)
        {
            _sqlConnection.Open();

            SqlCommand cmd = new SqlCommand(query, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@BookId", book.BookId);
            cmd.Parameters.AddWithValue("@Title", book.Title);
            cmd.Parameters.AddWithValue("@ISBN", book.ISBN);
            cmd.Parameters.AddWithValue("@AuthorId", book.AuthorId);
            cmd.Parameters.AddWithValue("@PublisherId", book.PublisherId);
            cmd.Parameters.AddWithValue("@PublishedYear", book.PublishedYear);
            cmd.Parameters.AddWithValue("@CategoryId", book.CategoryId);
            cmd.Parameters.AddWithValue("@CoverURL", book.CoverURL);

            cmd.ExecuteNonQuery();

            _sqlConnection.Close();
        }
        
        private List<Book> ConvertBookDataTableToBooList(DataTable booksTable)
        {
            List<Book> bookList = new List<Book>();

            foreach (DataRow bookRow in booksTable.Rows)
            {
                Book book = new Book();
                book.BookId = (int)bookRow["BookId"];
                book.Title = (string)bookRow["Title"];
                book.ISBN = (string)bookRow["ISBN"];
                book.AuthorId = (int)bookRow["AuthorId"];
                book.AuthorName = (string)bookRow["AuthorFullName"];
                book.PublisherId = (int)bookRow["PublisherId"];
                book.PublisherName = (string)bookRow["PublisherName"];
                book.CategoryId = (int)bookRow["CategoryId"];
                book.CategoryName = (string)bookRow["CategoryName"];
                book.PublishedYear = (int)bookRow["PublishedYear"];
                book.CoverURL = (string)bookRow["CoverURL"];

                bookList.Add(book);
            }

            return bookList;
        }
    }
}
