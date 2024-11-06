using System.Collections.Generic;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetCategoryList();

        List<Category> GetFilteredCategoryList(string searchString);
        
        void InsertCategory(Category category);

        void UpdateCategory(Category category);

        void DeleteCategory(Category category);
    }
}
