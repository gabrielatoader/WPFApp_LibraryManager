using System.Collections.Generic;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Interfaces
{
    public interface IBookRepository
    {
        List<Book> GetAllBooksList();

        List<Book> GetFilteredBookList(
            string searchString,
            bool searchInTitle,
            bool searchInAuthor,
            bool searchInPublisher,
            bool searchInISBN,
            bool searchInCategory
            );

        List<Book> GetFilteredBooksByAuthor(int authorId);

        List<Book> GetFilteredBooksByPublisher(int publisherId);

        List<Book> GetFilteredBooksByCategory(int categoryId);

        void InsertNewBook(Book book);

        void UpdateBook(Book book);

        void DeleteBook(Book book);
    }
}