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

        void InsertNewBookInDb(Book book);
    }
}
