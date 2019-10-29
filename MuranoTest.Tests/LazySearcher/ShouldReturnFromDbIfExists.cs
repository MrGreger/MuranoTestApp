using MuranoTest.Data.Repositories;
using MuranoTest.Tests.Helpers;
using MuranoTestApp.Services.SearchServices;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MuranoTest.Tests.LazySearcher
{
    [TestFixture]
    public class ShouldReturnFromDbIfExists
    {
        [Test]
        public void ServiceReturnsResultFromDb()
        {

            TestSearcherProvider provider = new TestSearcherProvider().AddSearcher(new BasicSearcher("first searher"))
                                                                      .AddSearcher(new BasicSearcher("second searcher"));

            var repo = TestRepoHelper.CreateRepoWithQuerry("hello world");

            LazySearchService service = new LazySearchService(provider, repo);

            var result = service.SearchAsync("hello world").GetAwaiter().GetResult();

            Assert.IsTrue(!result.Any(x => x.Title.Contains("first searher")) && !result.Any(x => x.Title.Contains("second searcher")));
        }
    }
}
