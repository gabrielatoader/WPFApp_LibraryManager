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
            return _bookRepository.GetAllBooksList();
        }
        
        public List<Book> GetFilteredBooksByAuthor(int authorId)
        {
            return _bookRepository.GetFilteredBooksByAuthor(authorId);
        }

        public List<Book> GetFilteredBooksByPublisher(int publisherId)
        {
            return _bookRepository.GetFilteredBooksByPublisher(publisherId);
        }
        
        public List<Book> GetFilteredBooksByCategory(int categoryId)
        {
            return _bookRepository.GetFilteredBooksByCategory(categoryId);
        }
        
        public bool InsertNewBook(Book book) 
        {
            if (_bookValidator.IsValidBook(book))
            {
                _bookRepository.InsertNewBook(book);

                MessageBox.Show("Book added successfully!");

                return true;
            }

            return false;
        }

        public bool UpdateBook(Book book) 
        {
            if (_bookValidator.IsValidBook(book))
            {
                _bookRepository.UpdateBook(book);
                
                MessageBox.Show("Book updated successfully!");

                return true;
            }

            return false;
        }
    }
}