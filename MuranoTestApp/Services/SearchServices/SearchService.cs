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
    public abstract class SearchService : ISearchService
    {
        protected ISearchersProvider _searcherProvider;
        protected ISearchResultsRepository _searchResultsRepository;

        public SearchService(ISearchersProvider searcherProvider, ISearchResultsRepository searchResultsRepository)
        {
            _searcherProvider = searcherProvider;
            _searchResultsRepository = searchResultsRepository;
        }

        public abstract IEnumerable<SearchResult> GetSaved(string textForSearch);

        public virtual async Task<IEnumerable<SearchResult>> SearchAsync(string textForSearch)
        {
            var results = await GetFromSearchersAsync(textForSearch);

            if (results != null)
            {
                _searchResultsRepository.SetResults(textForSearch, results);
            }

            return results;
        }

        protected async Task<IEnumerable<SearchResult>> GetFromSearchersAsync(string textForSearch)
        {
            var services = _searcherProvider.GetSearchers().ToList();

            var searchTasks = new List<Task<IEnumerable<SearchResult>>>();

            var cancellationSource = new CancellationTokenSource();
            var cancellationToken = cancellationSource.Token;

            for (int i = 0; i < services.Count; i++)
            {
                searchTasks.Add(services[i].SearchAsync(textForSearch, cancellationToken));
            }

            Task<IEnumerable<SearchResult>> completedSearch = null;

            while (searchTasks.Count > 0)
            {
                completedSearch = await Task.WhenAny(searchTasks);

                if (completedSearch.Status == TaskStatus.RanToCompletion && completedSearch.Result?.Count() > 0)
                {
                    searchTasks.Remove(completedSearch);
                    cancellationSource.Cancel();
                    break;
                }
                else
                {
                    searchTasks.Remove(completedSearch);
                }
            }

            return completedSearch?.Result;
        }
    }
}
