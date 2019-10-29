using MuranoTest.Data.Models;
using MuranoTest.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MuranoTest.Tests
{
    public class TestSearchRepository : ISearchResultsRepository
    {
        private List<SearchResult> _searchResults = new List<SearchResult>();

        public IEnumerable<SearchResult> GetSearchResults(string queryText)
        {
            return _searchResults.Where(x => x.ForQuery == queryText);
        }

        public void SetResults(string queryText, IEnumerable<SearchResult> newSearchResults)
        {
            _searchResults.RemoveAll(x => x.ForQuery == queryText);

            for (int i = 0; i < newSearchResults.Count(); i++)
            {
                newSearchResults.ElementAt(i).ForQuery = queryText;
            }

            _searchResults.AddRange(newSearchResults);
        }
    }
}
