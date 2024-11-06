using System.Collections.Generic;
using System.Windows;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;
using WPFApp_LibraryManager.Repositories;
using WPFApp_LibraryManager.Utils;

namespace WPFApp_LibraryManager.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private ICategoryValidator _categoryValidator;

        public CategoryService(ICategoryRepository categoryRepository, ICategoryValidator categoryValidator)
        {
            _categoryRepository = categoryRepository;
            _categoryValidator = categoryValidator;
        }

        public List<Category> GetCategoryList()
        {
            List<Category> categoryList = new List<Category>();

            categoryList.AddRange(_categoryRepository.GetCategoryList());

            return categoryList;
        }

        public List<Category> GetCategoryListWithListHeader()
        {
            List<Category> categoryList = new List<Category>();

            Category header = new Category();
            header.Id = 0;
            header.Name = "- CATEGORY -";
            categoryList.Add(header);

            categoryList.AddRange(_categoryRepository.GetCategoryList());
            return categoryList;
        }

        public List<Category> GetFilteredCategoryList(string searchString) 
        {
            List<Category> categoryList = new List<Category>();

            categoryList.AddRange(_categoryRepository.GetFilteredCategoryList(searchString));

            return categoryList;
        }

        public bool InsertCategory(Category category)
        {
            if (_categoryValidator.IsValidCategory(category))
            {
                _categoryRepository.InsertCategory(category);

                MessageBox.Show("Category added successfully!");

                return true;
            }
            return false;
        }

        public bool UpdateCategory(Category category)
        {
            if (_categoryValidator.IsValidCategory(category))
            {
                _categoryRepository.UpdateCategory(category);

                MessageBox.Show("Category updated successfully!");

                return true;
            }

            return false;
        }

        public void DeleteCategory(Category category)
        {
            _categoryRepository.DeleteCategory(category);

            MessageBox.Show("Category deleted successfully!");
        }
    }
}