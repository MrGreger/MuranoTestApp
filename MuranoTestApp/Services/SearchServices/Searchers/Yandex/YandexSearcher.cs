using Microsoft.Extensions.Options;
using MuranoTest.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MuranoTestApp.Services.SearchServices.Searchers.Yandex
{
    public class YandexSearcher : ISearcher
    {
        private readonly YandexSearcherOptions _options;

        public YandexSearcher(IOptions<YandexSearcherOptions> options)
        {
            _options = options.Value;
        }

        public async Task<IEnumerable<SearchResult>> SearchAsync(string query, CancellationToken cancellationToken = default)
        {
            var encodedQuery = Uri.EscapeDataString(query);

            StringBuilder sb = new StringBuilder();

            sb.Append("https://yandex.com/search/xml?")
              .Append($"user={_options.Username}&")
              .Append($"key={_options.Key}&")
              .Append($"query={encodedQuery}&")
              .Append("sortby=rlv&filter=strict&maxpassages=1&")
              .Append("groupby=attr%3D%22%22.mode%3Dflat.groups-on-page%3D10.docs-in-group%3D1");

            var response = await Network.Client.GetAsync(sb.ToString(), cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var text = await response.Content.ReadAsStringAsync();

                var docs = XDocument.Parse(text).Element(XName.Get("yandexsearch"))?
                                               .Element(XName.Get("response"))?
                                               .Element(XName.Get("results"))?
                                               .Element(XName.Get("grouping"))?
                                               .Elements(XName.Get("group"))?
                                               .Select(x => x.Element(XName.Get("doc")));

                if(docs == null)
                {
                    return await Task.FromResult<IEnumerable<SearchResult>>(null);
                }

                var result = docs.Select(x =>
                {
                    var title = x.Element(XName.Get("title")).Value;
                    var url = x.Element(XName.Get("url")).Value;

                    return new SearchResult(query, url, title);
                });

                return result;
            }

            return await Task.FromResult<IEnumerable<SearchResult>>(null);
        }
    }
}
