using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;
using WPFApp_LibraryManager.Utils;

namespace WPFApp_LibraryManager.Repositories
{
    public class BookRepository : BaseRepository, IBookRepository
    {
        public List<Book> GetBookList()
        {
            return GetFilteredBookList(new BookFilters());
        }

        public List<Book> GetFilteredBookList(BookFilters bookFilters) 
        {
            SqlCommand cmd = new SqlCommand(SqlQueries.GetFilteredBookListQuery, _sqlConnection);
            cmd.CommandType = CommandType.Text;

            if (bookFilters.AuthorId == 0)
            {
                cmd.Parameters.AddWithValue("@AuthorId", DBNull.Value);
            }
            else 
            {
                cmd.Parameters.AddWithValue("@AuthorId", bookFilters.AuthorId);
            }

            if (bookFilters.CategoryId == 0)
            {
                cmd.Parameters.AddWithValue("@CategoryId", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CategoryId", bookFilters.CategoryId);
            }

            if (bookFilters.PublisherId == 0)
            {
                cmd.Parameters.AddWithValue("@PublisherId", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@PublisherId", bookFilters.PublisherId);
            }

            if (String.IsNullOrEmpty(bookFilters.SearchString))
            {
                cmd.Parameters.AddWithValue("@SearchString", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SearchString", bookFilters.SearchString);
            }

            cmd.Parameters.AddWithValue("@SearchInTitle", bookFilters.SearchInTitle);
            cmd.Parameters.AddWithValue("@SearchInAuthor", bookFilters.SearchInAuthor);
            cmd.Parameters.AddWithValue("@SearchInPublisher", bookFilters.SearchInPublisher);
            cmd.Parameters.AddWithValue("@SearchInISBN", bookFilters.SearchInISBN);
            cmd.Parameters.AddWithValue("@SearchInCategory", bookFilters.SearchInCategory);

            DataTable bookTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            using (sqlDataAdapter)
            {
                sqlDataAdapter.Fill(bookTable);
            }

            List<Book> bookList = new List<Book>();

            bookList = ConvertBookDataTableToBookList(bookTable);

            return bookList;
        }

        public void InsertBook(Book book)
        {
            try
            {
                _sqlConnection.Open();

                SqlCommand cmd = new SqlCommand(SqlQueries.InsertBookQuery, _sqlConnection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@ISBN", book.Isbn);
                cmd.Parameters.AddWithValue("@AuthorId", book.AuthorId);
                cmd.Parameters.AddWithValue("@PublisherId", book.PublisherId);
                cmd.Parameters.AddWithValue("@PublishedYear", book.PublishedYear);
                cmd.Parameters.AddWithValue("@CategoryId", book.CategoryId);
                cmd.Parameters.AddWithValue("@CoverURL", book.CoverURL);

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public void UpdateBook(Book book)
        {
            try
            {
                _sqlConnection.Open();

                SqlCommand cmd = new SqlCommand(SqlQueries.UpdateBookQuery, _sqlConnection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@BookId", book.Id);
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@ISBN", book.Isbn);
                cmd.Parameters.AddWithValue("@AuthorId", book.AuthorId);
                cmd.Parameters.AddWithValue("@PublisherId", book.PublisherId);
                cmd.Parameters.AddWithValue("@PublishedYear", book.PublishedYear);
                cmd.Parameters.AddWithValue("@CategoryId", book.CategoryId);
                cmd.Parameters.AddWithValue("@CoverURL", book.CoverURL);

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public void DeleteBook(int bookId)
        {
            try
            {
                _sqlConnection.Open();

                SqlCommand cmd = new SqlCommand(SqlQueries.DeleteBookQuery, _sqlConnection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@BookId", bookId);

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }

        }
        
        private List<Book> ConvertBookDataTableToBookList(DataTable bookTable)
        {
            List<Book> bookList = new List<Book>();

            foreach (DataRow bookRow in bookTable.Rows)
            {
                Book book = new Book();
                book.Id = (int)bookRow["BookId"];
                book.Title = (string)bookRow["Title"];
                book.Isbn = (string)bookRow["ISBN"];
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
        
        public bool IsBookIsbnInUse(Book book)
        {
            SqlCommand cmd = new SqlCommand(SqlQueries.IsBookIsbnInUseQuery, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@BookId", book.Id);
            cmd.Parameters.AddWithValue("@ISBN", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(book.Isbn.Trim()));

            DataTable resultTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            using (sqlDataAdapter)
            {
                sqlDataAdapter.Fill(resultTable);
            }

            if (resultTable.Rows.Count != 0)
            {
                return true;
            }

            return false;
        }
    }
}
