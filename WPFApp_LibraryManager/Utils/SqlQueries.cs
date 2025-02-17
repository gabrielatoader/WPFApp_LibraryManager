namespace WPFApp_LibraryManager.Utils
{
    public static class SqlQueries
    {
        public const string GetFilteredBookListQuery =
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
            WHERE (@AuthorId IS NULL OR @AuthorId = Author_Id) 
                AND (@CategoryId IS NULL OR @CategoryId = Category_Id) 
                AND (@PublisherId IS NULL OR @PublisherId = Publisher_Id)
                AND ((@SearchString IS NULL)
                    OR (@SearchInAuthor = 1 AND a.FirstName LIKE '%' + @SearchString + '%')
                    OR (@SearchInAuthor = 1 AND a.MiddleName LIKE '%' + @SearchString + '%')
                    OR (@SearchInAuthor = 1 AND a.LastName LIKE '%' + @SearchString + '%')
                    OR (@SearchInISBN = 1 AND ISBN LIKE '%' + @SearchString + '%')
                    OR (@SearchInTitle = 1 AND Title LIKE '%' + @SearchString + '%')
                    OR (@SearchInCategory = 1 AND c.Name LIKE '%' + @SearchString + '%')
                    OR (@SearchInPublisher = 1 AND p.Name LIKE '%' + @SearchString + '%'))";

        public const string InsertBookQuery =
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

        public const string GetAuthorListQuery =
            @"SELECT
                Id AS AuthorId, 
                FirstName, 
                MiddleName, 
                LastName, 
                CONCAT_WS(' ', [FirstName], [MiddleName], [LastName]) AS AuthorFullName 
            FROM 
                Authors";

        public const string GetFilteredAuthorListQuery =
            @"SELECT
                Id AS AuthorId, 
                FirstName, 
                MiddleName, 
                LastName, 
                CONCAT_WS(' ', [FirstName], [MiddleName], [LastName]) AS AuthorFullName 
            FROM 
                Authors
            WHERE
                @SearchString IS NULL 
                OR FirstName LIKE '%' + @SearchString + '%' 
                OR MiddleName LIKE '%' + @SearchString + '%' 
                OR LastName LIKE '%' + @SearchString + '%'";

        public const string InsertAuthorQuery =
            @"INSERT INTO 
                Authors (FirstName, MiddleName, LastName)
                VALUES (@AuthorFirstName, @AuthorMiddleName, @AuthorLastName)";

        public const string UpdateAuthorQuery =
            @"UPDATE Authors
            SET FirstName = @AuthorFirstName, 
                MiddleName =  @AuthorMiddleName,
                LastName = @AuthorLastName
            WHERE Id = @AuthorId";

        public const string IsAuthorInUseQuery =
            @"IF EXISTS 
            (
                SELECT TOP 1 * 
                FROM Books 
                WHERE Author_Id = @AuthorId
            )
                SELECT 'true'";

        public const string DeleteAuthorQuery =
            @"DELETE FROM Authors
            WHERE Id = @AuthorId";

        public const string GetCategoryListQuery = 
            @"SELECT 
                Id AS CategoryId,
                Name AS CategoryName,
                Description AS CategoryDescription
            FROM 
                Categories";

        public const string GetFilteredCategoryListQuery =
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

        public const string IsCategoryInUseQuery =
            @"IF EXISTS 
            (
                SELECT TOP 1 * 
                FROM Books 
                WHERE Category_Id = @CategoryId
            )
                SELECT 'true'";

        public const string DeleteCategoryQuery =
            @"DELETE FROM Categories
            WHERE Id = @CategoryId";

        public const string GetPublisherListQuery = 
            @"SELECT 
                Id AS PublisherId,
                Name AS PublisherName,
                Description AS PublisherDescription
            FROM 
                Publishers";

        public const string GetFilteredPublisherListQuery =
            @"SELECT
                Id AS PublisherId, 
                Name AS PublisherName, 
                Description AS PublisherDescription
            FROM 
                Publishers
            WHERE
                @SearchString IS NULL OR
                Name LIKE '%' + @SearchString + '%' OR
                Description  LIKE '%' + @SearchString + '%'";

        public const string InsertPublisherQuery =
            @"INSERT INTO 
                Publishers (Name, Description)
                VALUES (@PublisherName, @PublisherDescription)";

        public const string UpdatePublisherQuery =
            @"UPDATE Publishers
            SET Name = @PublisherName, 
                Description =  @PublisherDescription
            WHERE Id = @PublisherId";

        public const string IsPublisherInUseQuery =
            @"IF EXISTS 
            (
                SELECT TOP 1 * 
                FROM Books 
                WHERE Publisher_Id = @PublisherId
            )
                SELECT 'true'";

        public const string DeletePublisherQuery =
            @"DELETE FROM Publishers
            WHERE Id = @PublisherId";
    }
}