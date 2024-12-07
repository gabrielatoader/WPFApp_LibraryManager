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

            //DisableSearchButton();
            //DisableClearButton();
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
            DisableSearchButton();
            DisableClearButton();
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

        private void Search_Txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnableSearchButton();
            EnableClearButton();
        }

        private void CategoryList_Dtg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;

            var selectedRow = dataGrid.SelectedItem as Category;

            if (selectedRow != null)
            {
                BindCategoryToCategoryDetails(selectedRow);
                
                EnableClearButton();
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

            Style accentButtonStyle = TryFindResource("MahApps.Styles.Button.Square.Accent") as Style;

            Cancel_Btn.Style = accentButtonStyle;
            Save_Btn.Style = accentButtonStyle;
        }

        private void DisableSaveCancelButtons()
        {
            Cancel_Btn.IsEnabled = false;
            Save_Btn.IsEnabled = false;

            Style normalButtonStyle = TryFindResource("MahApps.Styles.Button.Square") as Style;

            Cancel_Btn.Style = normalButtonStyle;
            Save_Btn.Style = normalButtonStyle;
        }

        private void EnableEditDeleteButtons()
        {
            Edit_Btn.IsEnabled = true;
            Delete_Btn.IsEnabled = true;

            Style accentButtonStyle = TryFindResource("MahApps.Styles.Button.Square.Accent") as Style;

            Edit_Btn.Style = accentButtonStyle;
            Delete_Btn.Style = accentButtonStyle;
        }

        private void DisableEditDeleteButtons()
        {
            Edit_Btn.IsEnabled = false;
            Delete_Btn.IsEnabled = false;

            Style normalButtonStyle = TryFindResource("MahApps.Styles.Button.Square") as Style;

            Edit_Btn.Style = normalButtonStyle;
            Delete_Btn.Style = normalButtonStyle;
        }

        private void EnableSearchButton()
        {
            Search_Btn.IsEnabled = true;

            Style accentButtonStyle = TryFindResource("MahApps.Styles.Button.Square.Accent") as Style;

            Search_Btn.Style = accentButtonStyle;
        }

        private void DisableSearchButton()
        {
            Search_Btn.IsEnabled = false;

            Style normalButtonStyle = TryFindResource("MahApps.Styles.Button.Square") as Style;

            Search_Btn.Style = normalButtonStyle;
        }

        private void EnableClearButton()
        {
            Clear_Btn.IsEnabled = true;

            Style accentButtonStyle = TryFindResource("MahApps.Styles.Button.Square.Accent") as Style;

            Clear_Btn.Style = accentButtonStyle;
        }

        private void DisableClearButton()
        {
            Clear_Btn.IsEnabled = false;

            Style normalButtonStyle = TryFindResource("MahApps.Styles.Button.Square") as Style;

            Clear_Btn.Style = normalButtonStyle;
        }
    }
}
