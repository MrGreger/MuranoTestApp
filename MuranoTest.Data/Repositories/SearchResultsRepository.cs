using MuranoTest.Data.Db;
using MuranoTest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MuranoTest.Data.Repositories
{
    public class SearchResultsRepository : ISearchResultsRepository
    {
        private readonly ApplicationDbContext _db;

        public SearchResultsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<SearchResult> GetSearchResults(string queryText)
        {
            return _db.SearchResults.Where(x => x.ForQuery.Trim().ToLower().Equals(queryText.Trim().ToLower())).ToList();
        }

        public void SetResults(string queryText, IEnumerable<SearchResult> newSearchResults)
        {
            var results = GetSearchResults(queryText);

            if (results.Any())
            {
                _db.RemoveRange(results);
            }

            for (int i = 0; i < newSearchResults.Count(); i++)
            {
                newSearchResults.ElementAt(i).ForQuery = queryText;
            }

            _db.AddRange(newSearchResults);
            _db.SaveChanges();
        }
    }
}
