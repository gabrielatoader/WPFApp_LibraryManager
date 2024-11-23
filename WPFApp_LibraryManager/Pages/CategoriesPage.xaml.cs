using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Pages
{
    public partial class CategoriesPage : UserControl
    {
        private ICategoryService _categoryService;

        private string _requestType = "";

        public CategoriesPage(ICategoryService categoryService)
        {
            _categoryService = categoryService;

            InitializeComponent();

            BindCategoryListToGrid(_categoryService.GetCategoryList());
        }

        private void BindCategoryListToGrid(List<Category> categoryList)
        {
            CategoryList_Dtg.ItemsSource = categoryList;
        }

        private void ClearCategoryGrid()
        {
            BindCategoryListToGrid(_categoryService.GetCategoryList());
        }

        private void BindCategoryToCategoryDetails(Category category)
        {
            TargetCategory_Name_Txt.Text = category.Name;
            TargetCategory_Description_Txt.Text = category.Description;

            DisableCategoryDetails();
        }

        private void Clear_Btn_Click(object sender, RoutedEventArgs e)
        {
            _requestType = "";

            ClearSearch();
            ClearCategoryGrid();
            ClearCategoryDetails();
        }

        private void Save_Btn_Click(object sender, RoutedEventArgs e)
        {
            Category targetCategory = new Category();
            targetCategory.Name = TargetCategory_Name_Txt.Text;
            targetCategory.Description = TargetCategory_Description_Txt.Text;

            bool result = false;

            if (_requestType == "update")
            {
                Category activeCategory = (Category)CategoryList_Dtg.SelectedItem;

                targetCategory.Id = activeCategory.Id;

                result = _categoryService.UpdateCategory(targetCategory);
            }
            else if (_requestType == "insert")
            {
                result = _categoryService.InsertCategory(targetCategory);
            }
            else
            {
                MessageBox.Show("Something went wrong, try again.");
            }

            if (result == true)
            {
                Clear_Btn_Click(sender, e);
            }
        }

        private void Cancel_Btn_Click(object sender, RoutedEventArgs e)
        {
            _requestType = "";

            if ((Category)CategoryList_Dtg.SelectedItem != null)
            {
                BindCategoryToCategoryDetails((Category)CategoryList_Dtg.SelectedItem);
            }
            else
            {
                ClearCategoryDetails();
            }
        }

        private void Edit_Btn_Click(object sender, RoutedEventArgs e)
        {
            _requestType = "update";

            EnableCategoryDetails();
        }

        private void Delete_Btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Category activeCategory = (Category)CategoryList_Dtg.SelectedItem;

                _categoryService.DeleteCategory(activeCategory.Id);

                Clear_Btn_Click(sender, e);
            }
        }

        private void AddCategory_Btn_Click(object sender, RoutedEventArgs e)
        {
            _requestType = "insert";

            ClearCategoryDetails();

            EnableCategoryDetails();
        }

        private void Search_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(Search_Txt.Text))
            {
                MessageBox.Show("Search box is empty, please add a search string.");
            }
            else
            {
                List<Category> categoryList = _categoryService.GetFilteredCategoryList(Search_Txt.Text);

                if (categoryList == null || categoryList.Count == 0)
                {
                    MessageBox.Show("Could not find categories to match search request.");
                }
                else
                {
                    BindCategoryListToGrid(categoryList);
                }
            }
        }

        private void CategoryList_Dtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;

            var selectedRow = dataGrid.SelectedItem as Category;

            if (selectedRow != null)
            {
                BindCategoryToCategoryDetails(selectedRow);
            }
        }

        private void ClearSearch()
        {
            Search_Txt.Text = string.Empty;
        }

        private void EnableCategoryDetails()
        {
            TargetCategory_Name_Txt.IsReadOnly = false;
            TargetCategory_Description_Txt.IsReadOnly = false;

            EnableSaveCancelButtons();
            DisableEditDeleteButtons();
        }

        private void DisableCategoryDetails()
        {
            TargetCategory_Name_Txt.IsReadOnly = true;
            TargetCategory_Description_Txt.IsReadOnly = true;

            EnableEditDeleteButtons();
            DisableSaveCancelButtons();
        }

        private void ClearCategoryDetails()
        {
            TargetCategory_Name_Txt.Text = string.Empty;
            TargetCategory_Description_Txt.Text = string.Empty;

            DisableCategoryDetails();
            DisableEditDeleteButtons();
        }

        private void EnableSaveCancelButtons()
        {
            Cancel_Btn.IsEnabled = true;
            Save_Btn.IsEnabled = true;
        }

        private void DisableSaveCancelButtons()
        {
            Cancel_Btn.IsEnabled = false;
            Save_Btn.IsEnabled = false;
        }

        private void EnableEditDeleteButtons()
        {
            Edit_Btn.IsEnabled = true;
            Delete_Btn.IsEnabled = true;
        }

        private void DisableEditDeleteButtons()
        {
            Edit_Btn.IsEnabled = false;
            Delete_Btn.IsEnabled = false;
        }
    }
}
