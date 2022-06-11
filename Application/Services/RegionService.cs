using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using Database.Models;
using Application.Repository;
using Application.ViewModels;

namespace Application.Services
{
    public class RegionService
    {
        private readonly RegionRepository _repository;

        public RegionService(ApplicationContext dbContext)
        {
            _repository = new(dbContext);
        }

        public async Task Add(RegionViewModel viewModel)
        {
            Region region = new();
            region.Name = viewModel.Name;
            await _repository.AddAsync(region);
        }

        public async Task Update(RegionViewModel viewModel)
        {
            Region region = await _repository.GetByIdAsync(viewModel.Id);
            region.Id = viewModel.Id;
            region.Name = viewModel.Name;
            await _repository.UpdateAsync(region);
        }

        public async Task Delete(RegionViewModel viewModel)
        {
            Region region = await _repository.GetByIdAsync(viewModel.Id);
            await _repository.DeleteAsync(region);
        }

        public async Task<RegionViewModel> GetByIdViewModel(int id)
        {
            Region region = await _repository.GetByIdAsync(id);
            return new RegionViewModel
            {
                Id = region.Id,
                Name = region.Name
            };
        }

        public async Task<List<RegionViewModel>> GetAllViewModel()
        {
            var regions = await _repository.GetAllAsync();

            return regions.Select(region => new RegionViewModel
            {
                Id = region.Id,
                Name = region.Name
            }).ToList();
        }
    }
}
