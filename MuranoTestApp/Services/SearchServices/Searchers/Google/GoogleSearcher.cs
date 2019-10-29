using Microsoft.Extensions.Options;
using MuranoTest.Data.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MuranoTestApp.Services.SearchServices.Searchers.Google
{
    public class GoogleSearcher : ISearcher
    {
        private readonly GoogleSearcherOptions _options;

        public GoogleSearcher(IOptions<GoogleSearcherOptions> options)
        {
            _options = options.Value;
        }

        public async Task<IEnumerable<SearchResult>> SearchAsync(string query, CancellationToken cancellationToken = default)
        {
            var encodedQuery = Uri.EscapeDataString(query);

            StringBuilder sb = new StringBuilder();

            sb.Append("https://www.googleapis.com/customsearch/v1?")
              .Append($"key={_options.ApiKey}&")
              .Append($"cx={_options.CX}&")
              .Append($"q={encodedQuery}&")
              .Append($"prettyPrint=false&num=10");

            var response = await Network.Client.GetAsync(sb.ToString(), cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();

            var responseText = await response.Content.ReadAsStringAsync();

            JObject googleSearch = JObject.Parse(responseText);

            List<JToken> items = googleSearch["items"]?.Children().ToList();

            if(items == null)
            {
                return await Task.FromResult<IEnumerable<SearchResult>>(null);
            }

            return items.Select(x => new SearchResult(query, x["link"].Value<string>(), x["title"].Value<string>())); 
        }
    }
}
