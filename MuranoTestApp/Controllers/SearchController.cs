using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MuranoTest.Data.DTOs;
using MuranoTest.Data.Models;
using MuranoTestApp.Services.SearchServices;

namespace MuranoTestApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly IMapper _mapper;
        private ISearchService _searchService;

        public SearchController(IMapper mapper, ISearchService searchService)
        {
            _mapper = mapper;
            _searchService = searchService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new SearchDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string query)
        {
            var results = await _searchService.SearchAsync(query);

            if (results != null)
            {
                var dtos = results.Select(x => _mapper.Map<SearchResult, SearchResultDTO>(x));
                return View(new SearchDTO(query, dtos));
            }
            else
            {
                return View(new SearchDTO(query, new List<SearchResultDTO>()));
            }
           
        }

        [HttpGet]
        public IActionResult Saved()
        {
            return View(new SearchDTO());
        }

        [HttpPost]
        public IActionResult Saved(string query)
        {
            var results = _searchService.GetSaved(query);

            if (results != null)
            {
                var dtos = results.Select(x => _mapper.Map<SearchResult, SearchResultDTO>(x));
                return View(new SearchDTO(query, dtos));
            }
            else
            {
                return View(new SearchDTO(query, new List<SearchResultDTO>()));
            }

        }
    }
}