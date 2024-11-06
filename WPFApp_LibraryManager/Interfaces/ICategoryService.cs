using System.Collections.Generic;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Interfaces
{
    public interface ICategoryService
    {
        List<Category> GetCategoryList();

        List<Category> GetCategoryListWithListHeader();

        List<Category> GetFilteredCategoryList(string searchString);

        bool InsertCategory(Category category);

        bool UpdateCategory(Category category);

        void DeleteCategory(Category category);
    }
}
