﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly RegionService _regionService;

        public HomeController(ApplicationContext dbContext)
        {
            _pokemonService = new(dbContext);
            _regionService = new(dbContext);
            _viewModel = new();
        }

        public async Task<IActionResult> Index(int? RegionId)
        {
            if (RegionId != null)
            {
                _viewModel.PokemonViewModel = await _pokemonService.Filter(RegionId ?? default(int));
            }
            else
            {
                _viewModel.PokemonViewModel = await _pokemonService.GetAllViewModel();
            }
            
            _viewModel.Regions = await _regionService.GetAllViewModel();
            return View(_viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SearchByName(string PokemonName)
        {
            if (PokemonName == null)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            _viewModel.PokemonViewModel = await _pokemonService.Search(PokemonName);
            _viewModel.Regions = await _regionService.GetAllViewModel();
            return View("Index", _viewModel);
        }
    }
}