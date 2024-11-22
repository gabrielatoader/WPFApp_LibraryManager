using System;
using System.Windows;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Utils
{
    public class BookValidator : IBookValidator
    {
        public bool IsValidBook(Book book)
        {
            if (!IsValidTitle(book.Title))
            {
                MessageBox.Show("Title is empty. Please provide the book title!");

                return false;
            }
            else if (!IsValidISBN(book.ISBN))
            {
                MessageBox.Show("ISBN is not valid, it should have exactly 10 digits.");

                return false;
            }
            else if (!IsValidYear(book.PublishedYear))
            {
                MessageBox.Show("Year is not valid, it should have exactly 4 digits.");

                return false;
            }
            else if (!IsValidURL(book.CoverURL))
            {
                MessageBox.Show("Cover URL is not valid, please provide a proper link.");

                return false;
            }
            else if (!IsValidAuthor(book.AuthorId))
            {
                MessageBox.Show("Select the book author from the list!");

                return false;
            }
            else if (!IsValidCategory(book.CategoryId))
            {
                MessageBox.Show("Select the book category from the list!");

                return false;
            }
            else if (!IsValidPublisher(book.PublisherId))
            {
                MessageBox.Show("Select the book publisher from the list!");

                return false;
            }

            return true;
        }

        private bool IsValidTitle(string inputString)
        {
            if (String.IsNullOrWhiteSpace(inputString))
            {
                return false;
            }

            return true;
        }

        private bool IsValidISBN(string isbn)
        {
            if (IsDigitsOnly(isbn) && isbn.Length == 10)
            {
                return true;
            }

            return false;
        }

        private bool IsValidYear(int year)
        {
            if (year != -1 && year.ToString().Length == 4)
            {
                return true;
            }

            return false;
        }

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsValidURL(string urlInput)
        {
            Uri uriResult;

            if (Uri.TryCreate(urlInput, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            {
                return true;
            }

            return false;
        }

        private bool IsValidAuthor(int authorId)
        {
            if (authorId > 0)
            {
                return true;
            }

            return false;
        }

        private bool IsValidCategory(int categoryId)
        {
            if (categoryId > 0)
            {
                return true;
            }

            return false;
        }

        private bool IsValidPublisher(int publisherId)
        {
            if (publisherId > 0)
            {
                return true;
            }

            return false;
        }
    }
}
