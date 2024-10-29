using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;
using WPFApp_LibraryManager.Utils;

namespace WPFApp_LibraryManager.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SqlConnection _sqlConnection;

        public CategoryRepository()
        {
            _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["WPFApp_LibraryManager.Properties.Settings.LibraryManagerDBConnectionString"].ConnectionString);
        }

        public List<Category> GetCategoryList()
        {
            List<Category> categoryList = new List<Category>();

            DataTable categoriesTable = GetResultTable(SqlQueries.AllCategoriesQuery);

            foreach (DataRow categoryRow in categoriesTable.Rows)
            {
                Category category = new Category();
                category.Id = (int)categoryRow["CategoryId"];
                category.Name = (string)categoryRow["CategoryName"];

                categoryList.Add(category);
            }

            return categoryList;
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
