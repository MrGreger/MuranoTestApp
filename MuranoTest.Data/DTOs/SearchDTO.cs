using System;
using System.Collections.Generic;
using System.Text;

namespace MuranoTest.Data.DTOs
{
    public class SearchDTO
    {
        public string Query { get; }
        public IEnumerable<SearchResultDTO> Results { get;}

        public SearchDTO()
        {

        }

        public SearchDTO(string query, IEnumerable<SearchResultDTO> results)
        {
            Query = query;
            Results = results;
        }
    }
}
