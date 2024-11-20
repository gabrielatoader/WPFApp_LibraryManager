using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;
using WPFApp_LibraryManager.Utils;

namespace WPFApp_LibraryManager.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public List<Category> GetCategoryList()
        {
            List<Category> categoryList = new List<Category>();

            DataTable categoriesTable = GetResultTable(SqlQueries.AllCategoriesQuery);

            foreach (DataRow categoryRow in categoriesTable.Rows)
            {
                Category category = new Category();
                category.Id = (int)categoryRow["CategoryId"];
                category.Name = (string)categoryRow["CategoryName"];
                category.Description = (string)categoryRow["CategoryDescription"];

                categoryList.Add(category);
            }

            return categoryList;
        }
        public List<Category> GetFilteredCategoryList(string searchString)
        {
            SqlCommand cmd = new SqlCommand(SqlQueries.FilteredCategoryQuery, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@SearchString", searchString);

            DataTable categoriesTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            
            using (sqlDataAdapter)
            {
                sqlDataAdapter.Fill(categoriesTable);
            }

            List<Category> categoryList = new List<Category>();

            foreach (DataRow categoryRow in categoriesTable.Rows)
            {
                Category category = new Category();
                category.Id = (int)categoryRow["CategoryId"];
                category.Name = (string)categoryRow["CategoryName"];
                category.Description = (string)categoryRow["CategoryDescription"];

                categoryList.Add(category);
            }

            return categoryList;
        }
        
        public void InsertCategory(Category category)
        {
            _sqlConnection.Open();

            SqlCommand cmd = new SqlCommand(SqlQueries.InsertCategoryQuery, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@CategoryName", category.Name);
            cmd.Parameters.AddWithValue("@CategoryDescription", category.Description);

            cmd.ExecuteNonQuery();

            _sqlConnection.Close();
        }

        public void UpdateCategory(Category category)
        {
            _sqlConnection.Open();

            SqlCommand cmd = new SqlCommand(SqlQueries.UpdateCategoryQuery, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@CategoryId", category.Id);
            cmd.Parameters.AddWithValue("@CategoryName", category.Name);
            cmd.Parameters.AddWithValue("@CategoryDescription", category.Description);

            cmd.ExecuteNonQuery();

            _sqlConnection.Close();
        }

        public void DeleteCategory(int categoryId)
        {
            _sqlConnection.Open();

            SqlCommand cmd = new SqlCommand(SqlQueries.DeleteCategoryQuery, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@CategoryId", categoryId);

            cmd.ExecuteNonQuery();

            _sqlConnection.Close();
        }
    }
}
