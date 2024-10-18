using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFApp_LibraryManager.Models;
using WPFApp_LibraryManager.Utils;

namespace WPFApp_LibraryManager.Services
{
    public class AuthorService
    {
        public List<Author> GetAuthors()
        {
            List<Author> authorList = new List<Author>();

            Author header = new Author();
            header.Id = 0;
            header.FullName = "- AUTHOR -";
            authorList.Add(header);

            DataTable authorsTable = DbContext.GetResultTable(SqlQueries.AuthorsQuery);

            foreach (DataRow authorRow in authorsTable.Rows)
                {
                    Author author = new Author();

                    author.Id = (int)authorRow["Id"];
                    author.FullName = (string)authorRow["AuthorFullName"];

                    authorList.Add(author);
                }

                return authorList;
            }
        }
    }
}
