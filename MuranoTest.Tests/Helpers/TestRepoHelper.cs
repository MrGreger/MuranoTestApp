using MuranoTest.Data.Models;
using MuranoTest.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuranoTest.Tests.Helpers
{
    public class TestRepoHelper
    {
        public static ISearchResultsRepository CreateEmptyRepository()
        {
            return new TestSearchRepository();
        }

        public static ISearchResultsRepository CreateRepoWithQuerry(string queryText)
        {
            var repo = new TestSearchRepository();

            repo.SetResults(queryText, GenerateResults(queryText));

            return repo;
        }

        public static IEnumerable<SearchResult> GenerateResults(string queryText)
        {
            var results = new List<SearchResult>();

            for (int i = 0; i < 10; i++)
            {
                results.Add(new SearchResult(queryText, $"url{i}", $"title{i}"));
            }

            return results;           
        }

        public static IEnumerable<SearchResult> GenerateResults(string queryText, string searcherId)
        {
            var results = new List<SearchResult>();

            for (int i = 0; i < 10; i++)
            {
                results.Add(new SearchResult(queryText, $"url{i}", $"title{i} from {searcherId}"));
            }

            return results;
        }
    }
}
