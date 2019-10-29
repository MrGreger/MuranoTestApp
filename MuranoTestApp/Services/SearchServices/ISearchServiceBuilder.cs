using Microsoft.Extensions.DependencyInjection;
using System;

namespace MuranoTestApp.Services.SearchServices
{
    public interface ISearchServiceBuilder
    {
        public IServiceCollection ServiceCollection { get; }
    }
}