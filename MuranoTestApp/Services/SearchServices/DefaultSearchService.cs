using MuranoTest.Data.Models;
using MuranoTest.Data.Repositories;
using MuranoTestApp.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuranoTestApp.Services.SearchServices
{
    public class DefaultSearchService : SearchService
    {
        public DefaultSearchService(ISearchersProvider searcherProvider, ISearchResultsRepository searchResultsRepository)
                             : base(searcherProvider, searchResultsRepository)
        {
            _searcherProvider = searcherProvider;
            _searchResultsRepository = searchResultsRepository;
        }

        public override IEnumerable<SearchResult> GetSaved(string textForSearch)
        {
            return _searchResultsRepository.GetSearchResults(textForSearch);
        }

    }
}
