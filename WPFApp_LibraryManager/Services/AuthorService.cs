using System.Collections.Generic;
using System.Windows;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Services
{
    public class AuthorService : IAuthorService
    {
        private IAuthorRepository _authorRepository;
        private IAuthorValidator _authorValidator;

        public AuthorService(IAuthorRepository authorRepository, IAuthorValidator authorValidator)
        {
            _authorRepository = authorRepository;
            _authorValidator = authorValidator;
        }

        public List<Author> GetAuthorList()
        {
            List<Author> authorList = new List<Author>();

            authorList.AddRange(_authorRepository.GetAuthorList());

            return authorList;
        }

        public List<Author> GetAuthorListWithListHeader()
        {
            List<Author> authorList = new List<Author>();

            Author header = new Author();
            header.Id = 0;
            header.FullName = "- AUTHOR -";
            authorList.Add(header);

            authorList.AddRange(_authorRepository.GetAuthorList());
            return authorList;
        }

        public List<Author> GetFilteredAuthorList(string searchString)
        {
            List<Author> authorList = new List<Author>();

            authorList.AddRange(_authorRepository.GetFilteredAuthorList(searchString));
            return authorList;
        }

        public bool InsertAuthor(Author author)
        {
            if (_authorValidator.IsValidAuthor(author))
            {
                _authorRepository.InsertAuthor(author);

                MessageBox.Show("Author added successfully!");

                return true;
            }
            return false;
        }

        public bool UpdateAuthor(Author author)
        {
            if (_authorValidator.IsValidAuthor(author))
            {
                _authorRepository.UpdateAuthor(author);

                MessageBox.Show("Author updated successfully!");

                return true;
            }
            return false;
        }

        public void DeleteAuthor(int authorId)
        {
            _authorRepository.DeleteAuthor(authorId);

            MessageBox.Show("Author deleted successfully!");
        }
    }
}