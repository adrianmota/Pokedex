using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Repository;
using Application.ViewModels;
using Database;
using Database.Models;

namespace Application.Services
{
    public class PokemonService
    {
        private readonly PokemonRepository _repository;
        private readonly RegionService _regionService;
        private readonly PokemonTypeService _pokemonTypeService;

        public PokemonService(ApplicationContext dbContext)
        {
            _repository = new(dbContext);
            _regionService = new(dbContext);
            _pokemonTypeService = new(dbContext);
        }

        public async Task Add(SavePokemonViewModel viewModel)
        {
            Pokemon pokemon = new();
            pokemon.Name = viewModel.Name;
            pokemon.ImageUrl = viewModel.ImageUrl;
            pokemon.RegionId = viewModel.RegionId;
            pokemon.PrimaryTypeId = viewModel.PrimaryTypeId;
            pokemon.SecondaryTypeId = viewModel.SecondaryTypeId;

            await _repository.AddAsync(pokemon);
        }

        public async Task Edit(SavePokemonViewModel viewModel)
        {
            Pokemon pokemon = await _repository.GetByIdAsync(viewModel.Id);
            pokemon.Name = viewModel.Name;
            pokemon.ImageUrl = viewModel.ImageUrl;
            pokemon.RegionId = viewModel.RegionId;
            pokemon.PrimaryTypeId = viewModel.PrimaryTypeId;
            pokemon.SecondaryTypeId = viewModel.SecondaryTypeId;

            await _repository.UpdateAsync(pokemon);
        }

        public async Task Delete(int id)
        {
            Pokemon pokemon = await _repository.GetByIdAsync(id);
            await _repository.DeleteAsync(pokemon);
        }

        public async Task<List<PokemonViewModel>> Search(string name)
        {
            List<Pokemon> pokemon = await _repository.GetAllAsync();
            List<PokemonViewModel> pokemonViewModel = new();
            var regions = await _regionService.GetAllRegionViewModel();
            var pokemonTypes = await _pokemonTypeService.GetAllPokemonTypeViewModel();

            foreach (var pokem in pokemon)
            {
                if (pokem.Name.Equals(name))
                {
                    pokemonViewModel.Add(new PokemonViewModel
                    {
                        Id = pokem.Id,
                        Name = pokem.Name,
                        ImageUrl = pokem.ImageUrl,
                        Region = regions.Find(region => region.Id == pokem.RegionId),
                        PrimaryType = pokemonTypes.Find(pokemonType => pokemonType.Id == pokem.PrimaryTypeId),
                        SecondaryType = pokemonTypes.Find(pokemonType => pokemonType.Id == pokem.SecondaryTypeId)
                    });
                }
            }

            return pokemonViewModel;
        }

        public async Task<List<PokemonViewModel>> Filter(int regionId)
        {
            List<PokemonViewModel> viewModels = new();
            var regions = await _regionService.GetAllRegionViewModel();
            var pokemonTypes = await _pokemonTypeService.GetAllPokemonTypeViewModel();
            var pokemon = await _repository.GetAllAsync();
            
            foreach (Pokemon pok in pokemon)
            {
                if (pok.RegionId == regionId)
                {
                    viewModels.Add(new PokemonViewModel
                    {
                        Id = pok.Id,
                        Name = pok.Name,
                        ImageUrl = pok.ImageUrl,
                        Region = regions.Find(region => region.Id == pok.RegionId),
                        PrimaryType = pokemonTypes.Find(pokemonType => pokemonType.Id == pok.PrimaryTypeId),
                        SecondaryType = pokemonTypes.Find(pokemonType => pokemonType.Id == pok.SecondaryTypeId)
                    });
                }
            }

            return viewModels;
        }

        public async Task<List<PokemonViewModel>> GetAllViewModel()
        {
            var pokemon = await _repository.GetAllAsync();
            var pokemonViewModel = await GenerateViewModel(pokemon);

            return pokemonViewModel;
        }

        public async Task<SavePokemonViewModel> GetByIdSaveViewModel(int id)
        {
            var pokemon = await _repository.GetByIdAsync(id);
            return new SavePokemonViewModel
            {
                Id = pokemon.Id,
                Name = pokemon.Name,
                ImageUrl = pokemon.ImageUrl,
                RegionId = pokemon.RegionId,
                PrimaryTypeId = pokemon.PrimaryTypeId,
                SecondaryTypeId = pokemon.SecondaryTypeId
            };
        }

        private async Task<List<PokemonViewModel>> GenerateViewModel(List<Pokemon> pokemon)
        {
            var regions = await _regionService.GetAllRegionViewModel();
            var pokemonTypes = await _pokemonTypeService.GetAllPokemonTypeViewModel();

            var pokemonViewModel = pokemon.Select(pokemon => new PokemonViewModel
            {
                Id = pokemon.Id,
                Name = pokemon.Name,
                ImageUrl = pokemon.ImageUrl,
                Region = regions.Find(region => region.Id == pokemon.RegionId),
                PrimaryType = pokemonTypes.Find(pokemonType => pokemonType.Id == pokemon.PrimaryTypeId),
                SecondaryType = pokemonTypes.Find(pokemonType => pokemonType.Id == pokemon.SecondaryTypeId)
            }).ToList();

            return pokemonViewModel;
        }
    }
}
