using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.ViewModels;
using Database;
using System.Threading.Tasks;

namespace Pokedex.Controllers
{
    public class PokemonTypeController : Controller
    {
        private readonly PokemonTypeService _service;

        public PokemonTypeController(ApplicationContext dbContext)
        {
            _service = new(dbContext);
        }

        public async Task<IActionResult> Index()
        {
            var pokemonTypes = await _service.GetAllPokemonTypeViewModel();
            return View(pokemonTypes);
        }

        public IActionResult Create()
        {
            PokemonTypeViewModel viewModel = new();
            return View("SavePokemonType", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PokemonTypeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("SavePokemonType", viewModel);
            }

            await _service.Add(viewModel);
            return RedirectToRoute(new { controller = "PokemonType", action = "Index" });
        }

        public async Task<IActionResult> Edit(PokemonTypeViewModel viewModel)
        {
            ModelState.Clear();
            PokemonTypeViewModel pokemonType = await _service.GetByIdPokemonTypeViewModel(viewModel.Id);
            return View("SavePokemonType", pokemonType);
        }

        [HttpPost]
        public async Task<IActionResult> EditPost(PokemonTypeViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("SavePokemonType", viewModel);
            }

            await _service.Update(viewModel);
            return RedirectToRoute(new { controller = "PokemonType", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _service.GetByIdPokemonTypeViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(PokemonTypeViewModel viewModel)
        {
            await _service.Delete(viewModel);
            return RedirectToRoute(new { controller = "PokemonType", action = "Index" });
        }
    }
}
