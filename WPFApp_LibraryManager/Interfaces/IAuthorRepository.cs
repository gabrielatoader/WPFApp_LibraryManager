using System.Collections.Generic;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Interfaces
{
    public interface IAuthorRepository
    {
        List<Author> GetAuthorList();

        List<Author> GetFilteredAuthorList(string searchString);

        void InsertAuthor(Author author);

        void UpdateAuthor(Author author);

        void DeleteAuthor(int authorId);

        bool IsAuthorInUse(int authorId);

        bool IsAuthorNameInUse(Author author);
    }
}
