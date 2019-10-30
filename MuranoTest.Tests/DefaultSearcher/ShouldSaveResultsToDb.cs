using MuranoTest.Tests.Helpers;
using MuranoTestApp.Services.SearchServices;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MuranoTest.Tests.DefaultSearcher
{
    [TestFixture]
    public class ShouldSaveResultsToDb
    {
        [Test]
        public void ShouldReturnSaveResultsToDb()
        {
            TestSearcherProvider provider = new TestSearcherProvider().AddSearcher(new EmptyResultSearcher("empty searcher"))
                                                                    .AddSearcher(new SlowSearcher(4, "first searher"))
                                                                    .AddSearcher(new SlowSearcher(3, "second searcher"))
                                                                    .AddSearcher(new SlowSearcher(1, "fastest searcher"))
                                                                    .AddSearcher(new ErroredSearcher("errored searcher"));

            var repo = TestRepoHelper.CreateEmptyRepository();

            DefaultSearchService service = new DefaultSearchService(provider, repo);

            var result = service.SearchAsync("hello world").GetAwaiter().GetResult();

            Assert.IsTrue(service.GetSaved("hello world").All(x => x.Title.Contains("fastest searcher")));
            Assert.IsTrue(result.All(x => x.Title.Contains("fastest searcher")));
        }
    }
}
