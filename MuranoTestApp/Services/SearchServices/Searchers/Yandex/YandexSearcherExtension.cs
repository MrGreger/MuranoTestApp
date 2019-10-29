using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuranoTestApp.Services.SearchServices.Searchers.Yandex
{
    public static class YandexSearcherExtension
    {
        public static void AddYandexSearcher(this IServiceCollection services, Action<YandexSearcherOptions> configuration)
        {
            services.AddScoped<ISearcher, YandexSearcher>();
            services.Configure(configuration);
        }
    }
}
