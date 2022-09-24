using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Application.ViewModels;
using Database;
using System.Threading.Tasks;

namespace Pokedex.Controllers
{
    public class RegionController : Controller
    {
        private readonly RegionService _service;

        public RegionController(ApplicationContext dbContext)
        {
            _service = new(dbContext);
        }

        public async Task<IActionResult> Index()
        {
            var regions = await _service.GetAllViewModel();
            return View(regions);
        }

        public IActionResult Create()
        {
            RegionViewModel viewModel = new RegionViewModel();
            return View("SaveRegion", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegionViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveRegion", viewModel);
            }

            await _service.Add(viewModel);
            return RedirectToRoute(new { controller = "Region", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            var region = await _service.GetByIdViewModel(id);
            return View("SaveRegion", region);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveRegion", viewModel);
            }

            await _service.Update(viewModel);
            return RedirectToRoute(new { controller = "Region", action = "Index" });
        }

        public IActionResult Delete(RegionViewModel viewModel)
        {
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(RegionViewModel viewModel)
        {
            await _service.Delete(viewModel);
            return RedirectToRoute(new { controller = "Region", action = "Index" });
        }
    }
}