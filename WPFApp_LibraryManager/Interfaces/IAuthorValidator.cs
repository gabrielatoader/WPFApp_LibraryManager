using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Interfaces
{
    public interface IAuthorValidator
    {
        bool IsValidAuthor(Author author);
    }
}
