using MuranoTestApp.Services.Helpers;
using MuranoTestApp.Services.SearchServices.Searchers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuranoTest.Tests.Helpers
{
    public class TestSearcherProvider : ISearchersProvider
    {
        private List<ISearcher> _searchers = new List<ISearcher>();

        public TestSearcherProvider AddSearcher(ISearcher searcher)
        {
            _searchers.Add(searcher);
            return this;
        }

        public IEnumerable<ISearcher> GetSearchers()
        {
            return _searchers;
        }
    }
}
