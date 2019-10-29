using Microsoft.Extensions.DependencyInjection;
using MuranoTestApp.Services.SearchServices.Searchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuranoTestApp.Services.Helpers
{
    public class DefaultSearcherProvider : ISearchersProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public DefaultSearcherProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<ISearcher> GetSearchers()
        {
            return _serviceProvider.GetServices<ISearcher>();
        }
    }
}
