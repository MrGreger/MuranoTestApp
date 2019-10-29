using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuranoTest.Data.Models
{
    public class SearchResult
    {
        public SearchResult()
        {

        }

        public SearchResult(string forQuery, string url, string title)
        {
            ForQuery = forQuery;
            Url = url;
            Title = title;
        }

        public int Id { get; set; }
        public string ForQuery { get; set; }
        public string Url { get; set; }
        public string  Title { get; set; }
    }
}
