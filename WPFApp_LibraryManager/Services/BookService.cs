using System;
using System.Collections.Generic;
using System.Windows;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Services
{
    public class BookService : IBookService
    {
        private IBookRepository _bookRepository;
        private IBookValidator _bookValidator;

        public BookService(IBookRepository bookRepository, IBookValidator bookValidator)
        {
            _bookRepository = bookRepository;
            _bookValidator = bookValidator;
        }

        public List<Book> GetAllBooksList()
        {
            List<Book> bookList = new List<Book>();

            try
            {
                bookList = _bookRepository.GetAllBooksList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not get book list: {ex.Message}");
            }

            return bookList;
        }

        public List<Book> GetFilteredBooksByAuthor(int authorId)
        {
            List<Book> bookList = new List<Book>();

            try
            {
                bookList = _bookRepository.GetFilteredBooksByAuthor(authorId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not get filtered book list: {ex.Message}");
            }

            return bookList;
        }

        public List<Book> GetFilteredBooksByPublisher(int publisherId)
        {
            List<Book> bookList = new List<Book>();

            try
            {
                bookList = _bookRepository.GetFilteredBooksByPublisher(publisherId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not get filtered book list: {ex.Message}");
            }

            return bookList;
        }

        public List<Book> GetFilteredBooksByCategory(int categoryId)
        {
            List<Book> bookList = new List<Book>();

            try
            {
                bookList = _bookRepository.GetFilteredBooksByCategory(categoryId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not get filtered book list: {ex.Message}");
            }

            return bookList;
        }

        public bool InsertNewBook(Book book)
        {
            if (_bookValidator.IsValidBook(book))
            {
                try
                {
                    _bookRepository.InsertNewBook(book);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Could not insert book: {ex.Message}");
                }

                MessageBox.Show("Book added successfully!");

                return true;
            }

            return false;
        }

        public bool UpdateBook(Book book)
        {
            if (_bookValidator.IsValidBook(book))
            {
                try
                {
                    _bookRepository.UpdateBook(book);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Could not update book: {ex.Message}");
                }

                MessageBox.Show("Book updated successfully!");

                return true;
            }

            return false;
        }

        public void DeleteBook(int bookId)
        {
            try
            {
                _bookRepository.DeleteBook(bookId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not update book: {ex.Message}");
            }

            MessageBox.Show("Book deleted successfully!");
        }

        public List<Book> GetFilteredBookList(
            string searchString,
            bool searchInTitle,
            bool searchInAuthor,
            bool searchInPublisher,
            bool searchInISBN,
            bool searchInCategory
            )
        {
            List<Book> bookList = new List<Book>();

            try
            {
                bookList.AddRange(_bookRepository.GetFilteredBookList(
                searchString,
                searchInTitle,
                searchInAuthor,
                searchInPublisher,
                searchInISBN,
                searchInCategory
                ));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not update book: {ex.Message}");
            }

            return bookList;
        }
    }
}