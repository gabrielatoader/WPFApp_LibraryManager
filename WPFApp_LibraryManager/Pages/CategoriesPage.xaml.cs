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

            BindCategoriesToGrid(_categoryService.GetCategoryList());
        }

        private void BindCategoriesToGrid(List<Category> categoryList)
        {
            CategoryList_Dtg.ItemsSource = categoryList;
        }

        private void ResetDataGrid()
        {
            BindCategoriesToGrid(_categoryService.GetCategoryList());
        }

        private void BindCategoryToCategoryDetails(Category category)
        {
            TargetCategory_Name_Txt.Text = category.Name;
            TargetCategory_Description_Txt.Text = category.Description;
        }

        private void Clear_Btn_Click(object sender, RoutedEventArgs e)
        {
            _requestType = "";

            ClearSearch();
            ResetDataGrid();
            ClearActiveCategorySection();
            DisableActiveCategorySection();

            Edit_Btn.IsEnabled = false;
            Delete_Btn.IsEnabled = false;
            Save_Btn.IsEnabled = false;
            Cancel_Btn.IsEnabled = false;
            AddCategory_Btn.IsEnabled = true;
        }
        
        private void Save_Btn_Click(object sender, RoutedEventArgs e)
        {
            Category targetCategory = new Category();
            targetCategory.Name = TargetCategory_Name_Txt.Text;
            targetCategory.Description = TargetCategory_Description_Txt.Text;
            
            if (_requestType == "update")
            {
                Category activeCategory = (Category)CategoryList_Dtg.SelectedItem;
                targetCategory.Id = activeCategory.Id;

                bool result = _categoryService.UpdateCategory(targetCategory);

                if (result == true)
                {
                    Clear_Btn_Click(sender, e);

                    BindCategoryToCategoryDetails(targetCategory);
                }
            }
            else if (_requestType == "insert")
            {
                bool result = _categoryService.InsertCategory(targetCategory);

                if (result == true)
                {
                    Clear_Btn_Click(sender, e);

                    BindCategoryToCategoryDetails(targetCategory);
                }
            }
            else
            {
                MessageBox.Show("Something went wrong, try again.");
            }
        }

        private void Cancel_Btn_Click(object sender, RoutedEventArgs e)
        {
            _requestType = "";

            DisableActiveCategorySection();

            Edit_Btn.IsEnabled = true;
            Delete_Btn.IsEnabled = true;
            Save_Btn.IsEnabled = false;
            Cancel_Btn.IsEnabled = false;
        }

        private void Edit_Btn_Click(object sender, RoutedEventArgs e)
        {
            _requestType = "update";

            EnableActiveCategorySection();

            Edit_Btn.IsEnabled = false;
            Delete_Btn.IsEnabled = false;
            Save_Btn.IsEnabled = true;
            Cancel_Btn.IsEnabled = true;
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

            ClearActiveCategorySection();
            EnableActiveCategorySection();
            
            AddCategory_Btn.IsEnabled = false;
            Edit_Btn.IsEnabled = false;
            Delete_Btn.IsEnabled = false;
            Cancel_Btn.IsEnabled = true;
            Save_Btn.IsEnabled = true;
        }

        private void Search_Btn_Click(object sender, RoutedEventArgs e)
        {
            BindCategoriesToGrid(_categoryService.GetFilteredCategoryList(Search_Txt.Text));
        }
        
        private void CategoryList_Dtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            var selectedRow = dataGrid.SelectedItem as Category;

            if (selectedRow != null)
            {
                BindCategoryToCategoryDetails(selectedRow);

                Edit_Btn.IsEnabled = true;
                Delete_Btn.IsEnabled = true;
                Save_Btn.IsEnabled = false;
                Cancel_Btn.IsEnabled = false;
            }
        }

        private void ClearSearch()
        {
            Search_Txt.Text = string.Empty;
        }
        
        private void EnableActiveCategorySection()
        {
            TargetCategory_Name_Txt.IsEnabled = true;
            TargetCategory_Description_Txt.IsEnabled = true;
        }

        private void DisableActiveCategorySection()
        {
            TargetCategory_Name_Txt.IsEnabled = false;
            TargetCategory_Description_Txt.IsEnabled = false;
        }

        private void ClearActiveCategorySection()
        {
            TargetCategory_Name_Txt.Text = string.Empty;
            TargetCategory_Description_Txt.Text = string.Empty;
        }
    }
}
