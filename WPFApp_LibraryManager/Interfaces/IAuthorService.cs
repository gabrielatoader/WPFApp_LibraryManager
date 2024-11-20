using System.Collections.Generic;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Interfaces
{
    public interface IAuthorService
    {
        List<Author> GetAuthorList();

        List<Author> GetAuthorListWithListHeader();

        List<Author> GetFilteredAuthorList(string searchString);

        bool InsertAuthor(Author author);

        bool UpdateAuthor(Author author);

        void DeleteAuthor(int authorId);
    }
}
