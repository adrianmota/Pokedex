using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pokedex.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Application.ViewModels;
using Database;

namespace Pokedex.Controllers
{
    public class HomeController : Controller
    {
        private readonly PokemonService _pokemonService;
        private readonly HomeViewModel _viewModel;
        private readonly PokemonTypeService _pokemonTypeService;
        private readonly RegionService _regionService;

        public HomeController(ApplicationContext dbContext)
        {
            _pokemonService = new(dbContext);
            _pokemonTypeService = new(dbContext);
            _regionService = new(dbContext);
            _viewModel = new();
        }

        public async Task<IActionResult> Index()
        {
            _viewModel.PokemonViewModel = await _pokemonService.GetAllPokemonViewModel();
            return View(_viewModel);
        }

        public IActionResult FilterByRegion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchByName(string PokemonName)
        {
            _viewModel.PokemonViewModel = await _pokemonService.Search(PokemonName);
            return View("Index", _viewModel);
        }
    }
}
