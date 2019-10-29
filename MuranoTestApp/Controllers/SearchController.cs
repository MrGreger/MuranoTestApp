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
            ViewBag.Search = true;
            return View(new SearchDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string query)
        {
            if (query == null)
            {
                return View(new SearchDTO());
            }

            var results = await _searchService.SearchAsync(query);

            ViewBag.Search = true;

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
            ViewBag.Search = false;

            return View("Index", new SearchDTO());
        }

        [HttpPost]
        public IActionResult Saved(string query)
        {
            if (query == null)
            {
                return View("Index", new SearchDTO());
            }

            var results = _searchService.GetSaved(query);

            ViewBag.Search = false;

            if (results != null)
            {
                var dtos = results.Select(x => _mapper.Map<SearchResult, SearchResultDTO>(x));
                return View("Index", new SearchDTO(query, dtos));
            }
            else
            {
                return View("Index", new SearchDTO(query, new List<SearchResultDTO>()));
            }

        }
    }
}