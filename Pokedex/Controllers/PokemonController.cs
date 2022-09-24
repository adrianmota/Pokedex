using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.ViewModels;
using Database;
using System.Threading.Tasks;

namespace Pokedex.Controllers
{
    public class PokemonController : Controller
    {
        private readonly PokemonService _service;
        private readonly PokemonTypeService _pokemonTypeService;
        private readonly RegionService _regionService;

        public PokemonController(ApplicationContext dbContext)
        {
            _service = new(dbContext);
            _pokemonTypeService = new(dbContext);
            _regionService = new(dbContext);
        }

        public async Task<IActionResult> Index()
        {
            var pokemon = await _service.GetAllViewModel();
            return View(pokemon);
        }

        public async Task<IActionResult> Create()
        {
            SavePokemonViewModel viewModel = new();
            viewModel.Regions = await _regionService.GetAllViewModel();
            viewModel.PokemonTypes = await _pokemonTypeService.GetAllViewModel();
            return View("SavePokemon", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(SavePokemonViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Regions = await _regionService.GetAllViewModel();
                viewModel.PokemonTypes = await _pokemonTypeService.GetAllViewModel();
                return View("SavePokemon", viewModel);
            }

            await _service.Add(viewModel);
            return RedirectToRoute(new { controller = "Pokemon", action = "Index" });
        }

        public async Task<IActionResult> Edit(SavePokemonViewModel viewModel)
        {
            viewModel = await _service.GetByIdSaveViewModel(viewModel.Id);
            viewModel.Regions = await _regionService.GetAllViewModel();
            viewModel.PokemonTypes = await _pokemonTypeService.GetAllViewModel();
            ModelState.Clear();
            return View("SavePokemon", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditPost(SavePokemonViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Regions = await _regionService.GetAllViewModel();
                viewModel.PokemonTypes = await _pokemonTypeService.GetAllViewModel();
                return View("SavePokemon", viewModel);
            }

            await _service.Edit(viewModel);
            return RedirectToRoute(new { controller = "Pokemon", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _service.GetByIdSaveViewModel(id));
        }

        public async Task<IActionResult> DeletePost(SavePokemonViewModel viewModel)
        {
            await _service.Delete(viewModel.Id);
            return RedirectToRoute(new { controller = "Pokemon", action = "Index" });
        }
    }
}