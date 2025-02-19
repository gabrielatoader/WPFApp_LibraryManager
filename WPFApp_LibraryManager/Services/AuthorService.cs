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
                    if (!_authorRepository.IsAuthorNameInUse(author))
                    {
                        _authorRepository.InsertAuthor(author);

                        MessageBox.Show("Author added successfully!");

                        return true;
                    }
                    else
                    {
                        MessageBox.Show($"Could not insert author. Another author with the same full name already exists.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Could not insert author: {ex.Message}");
                }
            }

            return false;
        }

        public bool UpdateAuthor(Author author)
        {
            if (_authorValidator.IsValidAuthor(author))
            {
                try
                {
                    if (!_authorRepository.IsAuthorNameInUse(author))
                    {
                        _authorRepository.UpdateAuthor(author);

                        MessageBox.Show("Author updated successfully!");

                        return true;
                    }
                    else
                    {
                        MessageBox.Show($"Could not update author. Another author with the same name already exists.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Could not update author: {ex.Message}");
                }
            }

            return false;
        }

        public void DeleteAuthor(int authorId)
        {
            try
            {
                if (_authorRepository.IsAuthorInUse(authorId))
                {
                    MessageBox.Show($"Could not delete Author #{authorId}. Some books are still associated with it.");
                }
                else
                {
                    _authorRepository.DeleteAuthor(authorId);

                    MessageBox.Show($"Author #{authorId} deleted successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not delete Author #{authorId}: {ex.Message}");
            }
        }
    }
}