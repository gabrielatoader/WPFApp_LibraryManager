using System.Collections.Generic;
using System.Data;
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

                categoryList.Add(category);
            }

            return categoryList;
        }
    }
}
