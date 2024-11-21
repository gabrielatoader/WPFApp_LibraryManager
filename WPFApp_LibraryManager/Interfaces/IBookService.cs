using System.Collections.Generic;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Interfaces
{
    public interface IBookService
    {
        List<Book> GetBookList();

        List<Book> GetFilteredBookList(BookFilters bookFilters);
        
        bool InsertBook(Book book);

        bool UpdateBook(Book book);

        void DeleteBook(int bookId);
    }
}
