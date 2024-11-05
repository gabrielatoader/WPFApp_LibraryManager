using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Interfaces
{
    public interface IBookValidator
    {
        bool IsValidBook(Book book);
    }
}