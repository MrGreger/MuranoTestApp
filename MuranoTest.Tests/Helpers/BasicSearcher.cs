using MuranoTest.Data.Models;
using MuranoTestApp.Services.SearchServices.Searchers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MuranoTest.Tests.Helpers
{
    public class BasicSearcher : ISearcher, ITestSearcher
    {
        public string SearcherId { get; set; }

        public BasicSearcher(string searcherId)
        {
            SearcherId = searcherId;
        }

        public async Task<IEnumerable<SearchResult>> SearchAsync(string query, CancellationToken cancellationToken = default)
        {
            return TestRepoHelper.GenerateResults(query, SearcherId);
        }
    }
}
