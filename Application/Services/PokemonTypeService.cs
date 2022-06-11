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
    public class PokemonTypeService
    {
        private readonly PokemonTypeRepository _repository;

        public PokemonTypeService(ApplicationContext dbContext)
        {
            _repository = new(dbContext);
        }

        public async Task Add(PokemonTypeViewModel viewModel)
        {
            PokemonType pokemonType = new();
            pokemonType.Name = viewModel.Name;
            await _repository.AddAsync(pokemonType);
        }

        public async Task Update(PokemonTypeViewModel viewModel)
        {
            PokemonType pokemonType = await _repository.GetByIdAsync(viewModel.Id);
            pokemonType.Name = viewModel.Name;
            await _repository.UpdateAsync(pokemonType);
        }

        public async Task Delete(PokemonTypeViewModel viewModel)
        {
            PokemonType pokemonType = await _repository.GetByIdAsync(viewModel.Id);
            await _repository.DeleteAsync(pokemonType);
        }

        public async Task<List<PokemonTypeViewModel>> GetAllViewModel()
        {
            var pokemonTypes = await _repository.GetAllAsync();

            return pokemonTypes.Select(pokemonType => new PokemonTypeViewModel
            {
                Id = pokemonType.Id,
                Name = pokemonType.Name
            }).ToList();
        }

        public async Task<PokemonTypeViewModel> GetByIdViewModel(int id)
        {
            PokemonType pokemonType = await _repository.GetByIdAsync(id);
            return new PokemonTypeViewModel
            {
                Id = pokemonType.Id,
                Name = pokemonType.Name
            };
        }
    }
}
