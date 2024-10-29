using System.Collections.Generic;
using WPFApp_LibraryManager.Interfaces;
using WPFApp_LibraryManager.Models;

namespace WPFApp_LibraryManager.Services
{
    public class AuthorService : IAuthorService
    {
        private IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public List<Author> GetAuthors()
        {
            List<Author> authorList = new List<Author>();

            Author header = new Author();
            header.Id = 0;
            header.FullName = "- AUTHOR -";
            authorList.Add(header);

            authorList.AddRange(_authorRepository.GetAuthorList());

            return authorList;
        }
    }
}