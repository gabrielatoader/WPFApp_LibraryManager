using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;

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

            try
            {
                categoryList = _categoryRepository.GetCategoryList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not get category list: {ex.Message}");
            }

            return categoryList;
        }

        public List<Category> GetCategoryListWithListHeader()
        {
            List<Category> categoryList = new List<Category>();

            Category header = new Category();
            header.Id = 0;
            header.Name = "- CATEGORY -";
            categoryList.Add(header);

            try
            {
                categoryList.AddRange(_categoryRepository.GetCategoryList());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not get category list: {ex.Message}");
            }

            return categoryList;
        }

        public List<Category> GetFilteredCategoryList(string searchString)
        {
            List<Category> categoryList = new List<Category>();

            try
            {
                categoryList = _categoryRepository.GetFilteredCategoryList(searchString);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not get filtered category list: {ex.Message}");
            }

            return categoryList;
        }

        public bool InsertCategory(Category category)
        {
            if (_categoryValidator.IsValidCategory(category))
            {
                try
                {
                    _categoryRepository.InsertCategory(category);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Could not insert category: {ex.Message}");
                }

                MessageBox.Show("Category added successfully!");

                return true;
            }

            return false;
        }

        public bool UpdateCategory(Category category)
        {
            if (_categoryValidator.IsValidCategory(category))
            {
                try
                {
                    _categoryRepository.UpdateCategory(category);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Could not update category: {ex.Message}");
                }

                MessageBox.Show("Category updated successfully!");

                return true;
            }

            return false;
        }

        public void DeleteCategory(int categoryId)
        {
            try
            {
                if (_categoryRepository.IsCategoryInUse(categoryId))
                {
                    MessageBox.Show($"Could not delete Category #{categoryId}. Some books are still associated with it.");
                }
                else
                {
                    _categoryRepository.DeleteCategory(categoryId);

                    MessageBox.Show($"Category #{categoryId} deleted successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not delete Category #{categoryId}: {ex.Message}");
            }
        }
    }
}