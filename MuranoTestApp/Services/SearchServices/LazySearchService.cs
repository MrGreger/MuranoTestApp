using Microsoft.Extensions.DependencyInjection;
using MuranoTest.Data.Models;
using MuranoTest.Data.Repositories;
using MuranoTestApp.Services.Helpers;
using MuranoTestApp.Services.SearchServices.Searchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MuranoTestApp.Services.SearchServices
{
    public class LazySearchService : SearchService
    {

        public LazySearchService(ISearchersProvider searcherProvider, ISearchResultsRepository searchResultsRepository)
                          : base(searcherProvider, searchResultsRepository)
        {
            _searchResultsRepository = searchResultsRepository;
        }

        public override IEnumerable<SearchResult> GetSaved(string textForSearch)
        {
            return _searchResultsRepository.GetSearchResults(textForSearch);
        }

        public override async Task<IEnumerable<SearchResult>> SearchAsync(string textForSearch)
        {
            var saved = GetSaved(textForSearch);

            if (saved.Any())
            {
                return saved;
            }

            return await base.SearchAsync(textForSearch);
        }
    }
}
