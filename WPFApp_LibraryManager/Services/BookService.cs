using System.Collections.Generic;
using System.Data;
using WPFApp_LibraryManager.Models;
using WPFApp_LibraryManager.Utils;

namespace WPFApp_LibraryManager.Services
{
    public class BookService
    {
        public List<Book> GetAllBooksList()
        {
            DataTable booksTable = DbContext.GetResultTable(SqlQueries.AllBooksQuery);
            
            return ConvertBookDataTableToBooList(booksTable);
        }
        
        public List<Book> GetFilteredBooksByAuthor(int authorId)
        {
            DataTable booksTable = DbContext.GetResultTableFilteredByAuthor(SqlQueries.BooksFilteredByAuthorQuery, authorId);

            return ConvertBookDataTableToBooList(booksTable);

        }

        public List<Book> GetFilteredBooksByPublisher(int publisherId)
        {
            DataTable booksTable = DbContext.GetResultTableFilteredByPublisher(SqlQueries.BooksFilteredByPublisherQuery, publisherId);

            return ConvertBookDataTableToBooList(booksTable);

        }
        
        public List<Book> GetFilteredBooksByCategory(int categoryId)
        {
            DataTable booksTable = DbContext.GetResultTableFilteredByCategory(SqlQueries.BooksFilteredByCategoryQuery, categoryId);

            return ConvertBookDataTableToBooList(booksTable);

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

        public void InsertNewBookInDb(Book book) 
        { 
            DbContext.InsertBookInDb(SqlQueries.InsertNewBookQuery,  book);
        }        
    }
}