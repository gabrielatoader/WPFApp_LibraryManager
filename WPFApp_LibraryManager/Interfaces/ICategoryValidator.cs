using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Interfaces
{
    public interface ICategoryValidator
    {
        bool IsValidCategory(Category category);
    }
}
