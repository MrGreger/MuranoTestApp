using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuranoTestApp.Services.SearchServices.Searchers.Google
{
    public static class GoogleSearcherExtension
    {
        public static void AddGoogleSearcher(this IServiceCollection services,Action<GoogleSearcherOptions> config)
        {
            services.AddScoped<ISearcher, GoogleSearcher>();
            services.Configure(config);
        }
    }
}
