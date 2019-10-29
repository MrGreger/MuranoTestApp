using Microsoft.Azure.CognitiveServices.Search.WebSearch;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuranoTestApp.Services.SearchServices.Searchers.Bing
{
    public static class BingSearcherExtension
    {
        public static void AddBingSearcher(this IServiceCollection services, BingSearcherOptions config)
        {
            services.AddSingleton(new WebSearchClient(new ApiKeyServiceClientCredentials(config.SubscriptionKey)));
            services.AddScoped<ISearcher, BingSearcher>();
        }
    }
}
