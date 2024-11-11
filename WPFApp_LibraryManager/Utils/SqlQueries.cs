namespace WPFApp_LibraryManager.Utils
{
    public static class SqlQueries
    {
        public const string AllBooksQuery = 
            @"SELECT
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
                JOIN Categories c
                    ON b.Category_Id = c.Id";

        public const string AllAuthorsQuery = 
            @"SELECT
                Id AS AuthorId, 
                FirstName, 
                MiddleName, 
                LastName, 
                CONCAT_WS(' ', [FirstName], [MiddleName], [LastName]) AS AuthorFullName 
            FROM 
                Authors";

        public const string AllCategoriesQuery = 
            @"SELECT 
                Id AS CategoryId,
                Name AS CategoryName,
                Description AS CategoryDescription
            FROM 
                Categories";

        public const string AllPublishersQuery = 
            @"SELECT 
                Id AS PublisherId,
                Name AS PublisherName
            FROM 
                Publishers";

        public const string FilteredBookQuery =
            @"SELECT 
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
            FROM Books b 
            JOIN Publishers p ON b.Publisher_Id = p.Id 
            JOIN Authors a ON b.Author_Id = a.Id
            JOIN Categories c ON b.Category_Id = c.Id
            WHERE @SearchString IS NULL
                OR (@SearchInAuthor = 1 AND a.FirstName LIKE '%' + @SearchString + '%')
                OR (@SearchInAuthor = 1 AND a.MiddleName LIKE '%' + @SearchString + '%')
                OR (@SearchInAuthor = 1 AND a.LastName LIKE '%' + @SearchString + '%')
                OR (@SearchInISBN = 1 AND ISBN LIKE '%' + @SearchString + '%')
                OR (@SearchInTitle = 1 AND Title LIKE '%' + @SearchString + '%')
                OR (@SearchInCategory = 1 AND c.Name LIKE '%' + @SearchString + '%')
                OR (@SearchInPublisher = 1 AND p.Name LIKE '%' + @SearchString + '%')";

        public const string WhereClause_FilerByAuthor = "Author_Id = @AuthorId";

        public const string WhereClause_FilerByCategory = "Category_Id = @CategoryId";

        public const string WhereClause_FilerByPublisher = "Publisher_Id = @PublisherId";

        public const string BooksFilteredByAuthorQuery = AllBooksQuery + " WHERE " + WhereClause_FilerByAuthor;

        public const string BooksFilteredByCategoryQuery = AllBooksQuery + " WHERE " + WhereClause_FilerByCategory;

        public const string BooksFilteredByPublisherQuery = AllBooksQuery + " WHERE " + WhereClause_FilerByPublisher;

        public const string InsertNewBookQuery = 
            @"INSERT INTO 
                Books (Title, ISBN, Author_Id, Publisher_Id, Published_Year, Category_Id, Cover_URL)
                VALUES (@Title, @ISBN, @AuthorId, @PublisherId, @PublishedYear, @CategoryId, @CoverURL)";

        public const string UpdateBookQuery =
            @"UPDATE Books
            SET Title = @Title, 
                ISBN =  @ISBN,
                Author_Id = @AuthorId,
                Publisher_Id = @PublisherId,
                Published_Year = @PublishedYear,
                Category_Id = @CategoryId,
                Cover_URL = @CoverURL
            WHERE Id = @BookId";

        public const string DeleteBookQuery =
            @"DELETE FROM Books
            WHERE Id = @BookId";

        public const string FilteredCategoryQuery =
            @"SELECT
                Id AS CategoryId, 
                Name AS CategoryName, 
                Description AS CategoryDescription
            FROM 
                Categories
            WHERE
                @SearchString IS NULL OR
                Name LIKE '%' + @SearchString + '%' OR
                Description  LIKE '%' + @SearchString + '%'";

        public const string InsertCategoryQuery =
            @"INSERT INTO 
                Categories (Name, Description)
                VALUES (@CategoryName, @CategoryDescription)";

        public const string UpdateCategoryQuery =
            @"UPDATE Categories
            SET Name = @CategoryName, 
                Description =  @CategoryDescription
            WHERE Id = @CategoryId";

        public const string DeleteCategoryQuery =
            @"DELETE FROM Categories
            WHERE Id = @CategoryId";
    }
}