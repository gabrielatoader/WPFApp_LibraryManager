﻿using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;
using WPFApp_LibraryManager.Utils;

namespace WPFApp_LibraryManager.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public List<Category> GetCategoryList()
        {
            List<Category> categoryList = new List<Category>();

            DataTable categoryTable = GetResultTable(SqlQueries.GetCategoryListQuery);

            foreach (DataRow categoryRow in categoryTable.Rows)
            {
                Category category = new Category();
                category.Id = (int)categoryRow["CategoryId"];
                category.Name = (string)categoryRow["CategoryName"];
                category.Description = (string)categoryRow["CategoryDescription"];

                categoryList.Add(category);
            }

            return categoryList;
        }

        public List<Category> GetFilteredCategoryList(string searchString)
        {
            SqlCommand cmd = new SqlCommand(SqlQueries.GetFilteredCategoryListQuery, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@SearchString", searchString);

            DataTable categoryTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            using (sqlDataAdapter)
            {
                sqlDataAdapter.Fill(categoryTable);
            }

            List<Category> categoryList = new List<Category>();

            foreach (DataRow categoryRow in categoryTable.Rows)
            {
                Category category = new Category();
                category.Id = (int)categoryRow["CategoryId"];
                category.Name = (string)categoryRow["CategoryName"];
                category.Description = (string)categoryRow["CategoryDescription"];

                categoryList.Add(category);
            }

            return categoryList;
        }

        public void InsertCategory(Category category)
        {
            try
            {
                _sqlConnection.Open();

                SqlCommand cmd = new SqlCommand(SqlQueries.InsertCategoryQuery, _sqlConnection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@CategoryName", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(category.Name.Trim()));
                cmd.Parameters.AddWithValue("@CategoryDescription", category.Description.Trim());

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public void UpdateCategory(Category category)
        {
            try
            {
                _sqlConnection.Open();

                SqlCommand cmd = new SqlCommand(SqlQueries.UpdateCategoryQuery, _sqlConnection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@CategoryId", category.Id);
                cmd.Parameters.AddWithValue("@CategoryName", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(category.Name.Trim()));
                cmd.Parameters.AddWithValue("@CategoryDescription", category.Description.Trim());

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public void DeleteCategory(int categoryId)
        {
            try
            {
                _sqlConnection.Open();

                SqlCommand cmd = new SqlCommand(SqlQueries.DeleteCategoryQuery, _sqlConnection);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public bool IsCategoryInUse(int categoryId)
        {
            SqlCommand cmd = new SqlCommand(SqlQueries.IsCategoryInUseQuery, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@CategoryId", categoryId);

            DataTable resultTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            using (sqlDataAdapter)
            {
                sqlDataAdapter.Fill(resultTable);
            }

            if (resultTable.Rows.Count != 0)
            {
                return true;
            }

            return false;
        }

        public bool IsCategoryNameInUse(Category category)
        {
            SqlCommand cmd = new SqlCommand(SqlQueries.IsCategoryNameInUseQuery, _sqlConnection);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@CategoryId", category.Id);
            cmd.Parameters.AddWithValue("@CategoryName", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(category.Name.Trim()));

            DataTable resultTable = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);

            using (sqlDataAdapter)
            {
                sqlDataAdapter.Fill(resultTable);
            }

            if (resultTable.Rows.Count != 0)
            {
                return true;
            }

            return false;
        }
    }
}