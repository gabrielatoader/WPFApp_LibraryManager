using System.Collections.Generic;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public List<Category> GetCategories()
        {
            List<Category> categoryList = new List<Category>();

            Category header = new Category();
            header.Id = 0;
            header.Name = "- CATEGORY -";
            categoryList.Add(header);

            categoryList.AddRange(_categoryRepository.GetCategoryList());

            return categoryList;
        }
    }
}