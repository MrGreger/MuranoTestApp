using MuranoTestApp.Services.SearchServices.Searchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuranoTestApp.Services.Helpers
{
    public interface ISearchersProvider
    {
        IEnumerable<ISearcher> GetSearchers();
    }
}
