using MuranoTest.Data.Models;
using MuranoTestApp.Services.SearchServices.Searchers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MuranoTest.Tests.Helpers
{
    public class EmptyResultSearcher : ISearcher, ITestSearcher
    {
        public string SearcherId { get; }
        public EmptyResultSearcher(string searcherId)
        {
            SearcherId = searcherId;
        }

        public async Task<IEnumerable<SearchResult>> SearchAsync(string query, CancellationToken cancellationToken = default)
        {
            return new List<SearchResult>();
        }
    }
}
