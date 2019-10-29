using MuranoTest.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuranoTest.Data.Repositories
{
    public interface ISearchResultsRepository
    {
        IEnumerable<SearchResult> GetSearchResults(string queryText);
        void SetResults(string queryText, IEnumerable<SearchResult> newSearchResults);
    }
}
