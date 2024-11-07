using System.Collections.Generic;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Interfaces
{
    public interface IBookService
    {
        List<Book> GetAllBooksList();

        List<Book> GetFilteredBooksByAuthor(int authorId);

        List<Book> GetFilteredBooksByPublisher(int publisherId);

        List<Book> GetFilteredBooksByCategory(int categoryId);

        bool InsertNewBook(Book book);

        bool UpdateBook(Book book);

        void DeleteBook(Book book);
    }
}
