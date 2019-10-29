using MuranoTest.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MuranoTestApp.Services.SearchServices
{
    public interface ISearchService
    {
        Task<IEnumerable<SearchResult>> SearchAsync(string textForSearch);
        IEnumerable<SearchResult> GetSaved(string textForSearch);
    }
}