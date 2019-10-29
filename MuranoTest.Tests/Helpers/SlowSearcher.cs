using MuranoTest.Data.Models;
using MuranoTestApp.Services.SearchServices.Searchers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MuranoTest.Tests.Helpers
{
    public class SlowSearcher : ISearcher, ITestSearcher
    {
        private int _delay;

        public string SearcherId { get; }

        public SlowSearcher(int delayInSeconds, string searcherId)
        {
            _delay = delayInSeconds * 1000;
            SearcherId = searcherId;
        }

        public async Task<IEnumerable<SearchResult>> SearchAsync(string query, CancellationToken cancellationToken = default)
        {
            await Task.Delay(_delay, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();

            return TestRepoHelper.GenerateResults(query, SearcherId);
        }
    }
}
