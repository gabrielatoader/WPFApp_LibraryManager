using System.Collections.Generic;
using System.Data;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;
using WPFApp_LibraryManager.Utils;

namespace WPFApp_LibraryManager.Repositories
{
    public class AuthorRepository : BaseRepository, IAuthorRepository
    {
        public List<Author> GetAuthorList()
        {
            List<Author> authorList = new List<Author>();

            DataTable authorsTable = GetResultTable(SqlQueries.AllAuthorsQuery);

            foreach (DataRow authorRow in authorsTable.Rows)
            {
                Author author = new Author();
                author.Id = (int)authorRow["AuthorId"];
                author.FullName = (string)authorRow["AuthorFullName"];

                authorList.Add(author);
            }

            return authorList;
        }
    }
}
