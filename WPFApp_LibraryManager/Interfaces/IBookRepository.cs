using System.Collections.Generic;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Interfaces
{
    public interface IBookRepository
    {
        List<Book> GetBookList();

        List<Book> GetFilteredBookList(BookFilters bookFilters);

        void InsertBook(Book book);

        void UpdateBook(Book book);

        void DeleteBook(int bookId);

        bool IsBookIsbnInUse(Book book);
    }
}