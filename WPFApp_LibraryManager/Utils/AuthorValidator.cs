using System;
using System.Windows;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Utils
{
    internal class AuthorValidator : IAuthorValidator
    {
        public bool IsValidAuthor(Author author)
        {
            if (!IsValidCategoryName(author.FirstName))
            {
                MessageBox.Show("First name is invalid. Please provide a correct value for the author's first name.");

                return false;
            }
            else if (!IsValidCategoryName(author.LastName))
            {
                MessageBox.Show("Last name is invalid. Please provide a correct value for the author's last name.");

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
