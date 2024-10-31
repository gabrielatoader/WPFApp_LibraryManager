using System;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Windows;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Utils
{
    public class BookValidator
    {
        public bool isValidBook(Book book)
        {
            if (isValidTitle(book.Title) && 
                isValidISBN(book.ISBN) && 
                isValidYear(book.PublishedYear) && 
                isValidURL(book.CoverURL) &&
                isValidAuthor(book.AuthorId) &&
                isValidCategory(book.CategoryId) &&
                isValidPublisher(book.PublisherId))
            {
                return true;
            }
            else if (!isValidTitle(book.Title))
            {
                MessageBox.Show("Title is empty. Please provide the book title!");
                return false;
            }
            else if (!isValidISBN(book.ISBN))
            {
                MessageBox.Show("ISBN is not valid, it should have exactly 10 digits.");
                return false;
            }
            else if (!isValidYear(book.PublishedYear))
            {
                MessageBox.Show("Year is not valid, it should have exactly 4 digits.");
                return false;
            }
            else if (!isValidURL(book.CoverURL))
            {
                MessageBox.Show("Cover URL is not valid, please provide a proper link.");
                return false;
            }
            else if (!isValidAuthor(book.AuthorId))
            {
                MessageBox.Show("Select the book author from the list!");
                return false;
            }
            else if (!isValidCategory(book.CategoryId))
            {
                MessageBox.Show("Select the book category from the list!");
                return false;
            }
            else if (!isValidPublisher(book.PublisherId))
            {
                MessageBox.Show("Select the book publisher from the list!");
                return false;
            }
            else
            {
                MessageBox.Show("Something went wrong, cannot validate book. Please try again");
                return false;
            }
        }

        private bool isValidISBN(string isbn) 
        {
            if (isValidInteger(isbn) && isbn.Length == 10) 
            { 
                return true;
            }

            return false;
        }

        public bool isValidTitle(string inputString)
        {
            if (String.IsNullOrWhiteSpace(inputString))
            { 
                return false;
            }

            return true;
        }

        public bool isValidYear(int year) 
        {
            if (year.ToString().Length == 4)
            {
                return true;
            }

            return false;
        }

        public bool isValidInteger(string num)
        {
            try
            {
                Convert.ToInt32(num);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool isValidURL(string urlInput)
        {
            Uri uriResult;
            if (Uri.TryCreate(urlInput, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        private bool isValidAuthor(int authorId) 
        {
            if (authorId > 0)
            {
                return true;
            }
            else 
            { 
                return false;
            }
        }

        private bool isValidCategory(int categoryId)
        {
            if (categoryId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool isValidPublisher(int publisherId)
        {
            if (publisherId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
