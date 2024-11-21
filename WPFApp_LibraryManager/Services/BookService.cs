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

        public List<Book> GetBookList()
        {
            List<Book> bookList = new List<Book>();

            try
            {
                bookList = _bookRepository.GetBookList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not get book list: {ex.Message}");
            }

            return bookList;
        }

        public List<Book> GetFilteredBookList(BookFilters bookFilters)
        {
            List<Book> bookList = new List<Book>();

            try
            {
                bookList = _bookRepository.GetFilteredBookList(bookFilters);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not get filtered book list: {ex.Message}");
            }

            return bookList;
        }

        public bool InsertBook(Book book)
        {
            if (_bookValidator.IsValidBook(book))
            {
                try
                {
                    _bookRepository.InsertBook(book);
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
                MessageBox.Show($"Could not delete book: {ex.Message}");
            }

            MessageBox.Show("Book deleted successfully!");
        }
    }
}