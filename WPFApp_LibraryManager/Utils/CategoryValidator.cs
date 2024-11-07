using System;
using System.Windows;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Utils
{
    public class CategoryValidator : ICategoryValidator
    {
        public bool IsValidCategory(Category category)
        {
            if (!IsValidCategoryName(category.Name))
            {
                MessageBox.Show("Name is invalid. Please provide the category name.");
                return false;
            }

            return true;
        }
        
        private bool IsValidCategoryName(string inputString)
        {
            if (String.IsNullOrWhiteSpace(inputString))
            {
                return false;
            }

            return true;
        }
    }
}
