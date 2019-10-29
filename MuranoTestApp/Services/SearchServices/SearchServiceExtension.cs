using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuranoTestApp.Services.SearchServices
{
    public static class SearchServiceExtension
    {
        public static void AddLazySearchService(this IServiceCollection services)
        {
            services.AddScoped<ISearchService, LazySearchService>();
        }

        public static void AddSearchService(this IServiceCollection services)
        {
            services.AddScoped<ISearchService, DefaultSearchService>();
        }
    }
}
