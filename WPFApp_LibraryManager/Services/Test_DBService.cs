using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFApp_LibraryManager.Utils;

namespace WPFApp_LibraryManager.Services
{
    public class Test_DBService
    {

        public string GetBaseSqlSelectQuery(Enum queryMode)
        {
            string query = string.Empty;

            switch (queryMode)
            {
                case QueryMode.All:
                    query = SqlQueries.AllBooksQuery;
                    break;
                case QueryMode.Authors:
                    query = SqlQueries.AllAuthorsQuery;
                    break;
                case QueryMode.Books:
                    //code books
                    break;
                case QueryMode.Categories:
                    query = SqlQueries.AllCategoriesQuery;
                    break;
                case QueryMode.Publishers:
                    query = SqlQueries.AllPublishersQuery;
                    break;
            }

            return query;
        }

        public string GetSqlWhereClauseForSearchLocations(List<Enum> querySearchLocations, string searchString)
        {
            string query = string.Empty;

            if (!querySearchLocations.Contains(QuerySearchLocation.None))
            {
                foreach (Enum queryLocation in querySearchLocations)
                {
                    switch (queryLocation)
                    {
                        case QuerySearchLocation.Authors:
                            query = query + SqlQueries.WhereClause_AuthorNameContainsString;
                            break;
                        case QuerySearchLocation.Titles:
                            query = query + SqlQueries.WhereClause_TitleContainsString;
                            break;
                        case QuerySearchLocation.Categories:
                            query = query + SqlQueries.WhereClause_CategoryNameContainsString;
                            break;
                        case QuerySearchLocation.ISBNs:
                            query = query + SqlQueries.WhereClause_ISBNContainsString;
                            break;
                        case QuerySearchLocation.Publishers:
                            query = query + "PublisherName LIKE '%@SearchString%'";
                            break;
                    }

                    if (!(queryLocation == querySearchLocations.Last()))
                    {
                        query = query + " OR ";
                    }
                }
            }

            return query;
        }

        public string GetSqlWhereClauseForFilters(List<Enum> queryFilters)
        {
            string query = string.Empty;

            if (!queryFilters.Contains(QueryFilter.None))
            {
                foreach (Enum queryFilter in queryFilters)
                {
                    switch (queryFilter)
                    {
                        case QueryFilter.Authors:
                            query = query + SqlQueries.WhereClause_FilerByAuthor;
                            break;
                        case QueryFilter.Categories:
                            query = query + SqlQueries.WhereClause_FilerByCategory;
                            break;
                        case QueryFilter.Publishers:
                            query = query + SqlQueries.WhereClause_FilerByPublisher;
                            break;
                    }

                    if (!(queryFilter == queryFilters.Last()))
                    {
                        query = query + " AND ";
                    }
                }
            }

            return query;
        }

        public string GetSqlWhereClause(List<Enum> querySearchLocations, string searchString, List<Enum> queryFilters)
        {
            string query = string.Empty;

            if (!(queryFilters.Contains(QueryFilter.None)) && !(querySearchLocations.Contains(QuerySearchLocation.None)))
            {
                query = query + " WHERE (" + GetSqlWhereClauseForFilters(queryFilters) + ") AND (" + GetSqlWhereClauseForSearchLocations(querySearchLocations, searchString) + ")";
            }
            else if (!queryFilters.Contains(QueryFilter.None))
            {
                query = query + " WHERE " + GetSqlWhereClauseForFilters(queryFilters);
            }
            else
            {
                query = query + " WHERE " + GetSqlWhereClauseForSearchLocations(querySearchLocations, searchString);
            }

            return query;
        }

        public string GenerateSqlQuery(Enum queryMode, List<Enum> querySearchLocations, string searchString, List<Enum> queryFilters)
        {
            string query = string.Empty;

            query = GetBaseSqlSelectQuery(queryMode);
            query = query + GetSqlWhereClause(querySearchLocations, searchString, queryFilters);

            return query;
        }

        public string TestSqlQuery()
        {
            List<Enum> querySearchLocations = new List<Enum>();
            //querySearchLocations.Add(QuerySearchLocation.None);
            querySearchLocations.Add(QuerySearchLocation.Authors);
            querySearchLocations.Add(QuerySearchLocation.ISBNs);
            querySearchLocations.Add(QuerySearchLocation.Titles);
            querySearchLocations.Add(QuerySearchLocation.Categories);
            querySearchLocations.Add(QuerySearchLocation.Publishers);

            List<Enum> queryFilters = new List<Enum>();
            //queryFilters.Add(QueryFilter.None);
            queryFilters.Add(QueryFilter.Authors);
            queryFilters.Add(QueryFilter.Categories);
            queryFilters.Add(QueryFilter.Publishers);

            return GenerateSqlQuery(QueryMode.All, querySearchLocations,string.Empty, queryFilters);

        }
    }
}
