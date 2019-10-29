using Microsoft.Azure.CognitiveServices.Search.WebSearch;
using Microsoft.Extensions.Options;
using MuranoTest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MuranoTestApp.Services.SearchServices.Searchers.Bing
{
    public class BingSearcher : ISearcher
    {
        private readonly WebSearchClient _webSearchClient;

        public BingSearcher(WebSearchClient webSearchClient)
        {
            _webSearchClient = webSearchClient;
        }

        public async Task<IEnumerable<SearchResult>> SearchAsync(string query, CancellationToken cancellationToken = default)
        {
            var response = await _webSearchClient.Web.SearchAsync(query, answerCount: 10, cancellationToken: cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();

            return response.WebPages?.Value?.Select(x => new SearchResult(query, x.Url, x.Name)) 
                ?? await Task.FromResult<IEnumerable<SearchResult>>(null);
        }
    }
}
