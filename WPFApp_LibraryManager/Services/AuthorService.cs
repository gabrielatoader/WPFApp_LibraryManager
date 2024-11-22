using System;
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

            try
            {
                authorList = _authorRepository.GetAuthorList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not get author list: {ex.Message}");
            }

            return authorList;
        }

        public List<Author> GetAuthorListWithListHeader()
        {
            List<Author> authorList = new List<Author>();

            Author header = new Author();
            header.Id = 0;
            header.FullName = "- AUTHOR -";
            authorList.Add(header);

            try
            {
                authorList.AddRange(_authorRepository.GetAuthorList());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not get author list: {ex.Message}");
            }

            return authorList;
        }

        public List<Author> GetFilteredAuthorList(string searchString)
        {
            List<Author> authorList = new List<Author>();

            try
            {
                authorList = _authorRepository.GetFilteredAuthorList(searchString);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not get filtered author list: {ex.Message}");
            }

            return authorList;
        }

        public bool InsertAuthor(Author author)
        {
            if (_authorValidator.IsValidAuthor(author))
            {
                try
                {
                    _authorRepository.InsertAuthor(author);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Could not insert author: {ex.Message}");
                }

                MessageBox.Show("Author added successfully!");

                return true;
            }

            return false;
        }

        public bool UpdateAuthor(Author author)
        {
            if (_authorValidator.IsValidAuthor(author))
            {
                try
                {
                    _authorRepository.UpdateAuthor(author);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Could not update author: {ex.Message}");
                }

                MessageBox.Show("Author updated successfully!");

                return true;
            }

            return false;
        }

        public void DeleteAuthor(int authorId)
        {
            try
            {
                _authorRepository.DeleteAuthor(authorId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not delete author: {ex.Message}");
            }

            MessageBox.Show("Author deleted successfully!");
        }
    }
}