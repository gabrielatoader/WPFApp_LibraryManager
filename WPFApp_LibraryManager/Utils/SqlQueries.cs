using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApp_LibraryManager.Utils
{
    public static class SqlQueries
    {
        public const string BookDetailsQuery = @"SELECT 
	                            b.Id as BookId, 
	                            ISBN, 
	                            Title, 
	                            Author_Id AS AuthorId, 
	                            CONCAT_WS(' ', [FirstName], [MiddleName], [LastName]) AS AuthorFullName,  
	                            Publisher_Id AS PublisherId, 
	                            p.Name AS PublisherName, 
	                            Published_Year as PublishedYear, 
	                            Category_Id AS CategoryId, 
	                            c.Name AS CategoryName, 
	                            Cover_URL AS CoverURL
                            FROM 
	                            Books b 
	                            JOIN Publishers p 
		                            ON b.Publisher_Id = p.Id 
	                            JOIN Authors a 
		                            ON b.Author_Id = a.Id
	                            JOIN Category c
		                            ON b.Category_Id = c.Id";

		public const string AuthorsQuery = @"SELECT 
                                Id, 
                                FirstName, 
                                MiddleName, 
                                LastName, 
                                CONCAT_WS(' ', [FirstName], [MiddleName], [LastName]) AS AuthorFullName 
                            FROM 
                                Authors";

		public const string CategoriesQuery = @"SELECT 
                                Id, 
                                Name AS CategoryName
                            FROM 
                                Categories";

		public const string PublishersQuery = @"SELECT 
                                Id, 
                                Name AS PublisherName
                            FROM 
                                Publishers";
    }
}
