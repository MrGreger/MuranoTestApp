using MuranoTest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MuranoTestApp.Services.SearchServices.Searchers
{
    public interface ISearcher
    {
        public Task<IEnumerable<SearchResult>> SearchAsync(string query, CancellationToken cancellationToken = default(CancellationToken));
    }
}
