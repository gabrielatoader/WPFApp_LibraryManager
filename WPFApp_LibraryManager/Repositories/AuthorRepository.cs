using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;
using WPFApp_LibraryManager.Utils;

namespace WPFApp_LibraryManager.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly SqlConnection _sqlConnection;

        public AuthorRepository()
        {
            _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["WPFApp_LibraryManager.Properties.Settings.LibraryManagerDBConnectionString"].ConnectionString);
        }
        
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

        private DataTable GetResultTable(string query)
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, _sqlConnection);

            using (sqlDataAdapter)
            {
                DataTable resultsTable = new DataTable();

                sqlDataAdapter.Fill(resultsTable);

                return resultsTable;
            }
        }
    }
}
