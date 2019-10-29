using AutoMapper;
using MuranoTest.Data.DTOs;
using MuranoTest.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MuranoTestApp.Profiles
{
    public class SearchResultProfile : Profile
    {
        public SearchResultProfile()
        {
            CreateMap<SearchResult, SearchResultDTO>();
        }
    }
}
