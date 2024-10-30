using System.Collections.Generic;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Services
{
    public class BookService : IBookService
    {
        private IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
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
        
        public void InsertNewBookInDb(Book book) 
        {
            _bookRepository.InsertNewBookInDb(book);
        }        
    }
}