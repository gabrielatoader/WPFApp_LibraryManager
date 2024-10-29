using System.Collections.Generic;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetCategoryList();
    }
}
