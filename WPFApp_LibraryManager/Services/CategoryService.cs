using System.Collections.Generic;
using System.Data;
using WPFApp_LibraryManager.Models;
using WPFApp_LibraryManager.Utils;

namespace WPFApp_LibraryManager.Services
{
    public class CategoryService
    {
        public List<Category> GetCategories()
        {
            List<Category> categoryList = new List<Category>();

            Category header = new Category();
            header.Id = 0;
            header.Name = "- CATEGORY -";
            categoryList.Add(header);

            DataTable categoriesTable = DbContext.GetResultTable(SqlQueries.AllCategoriesQuery);

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