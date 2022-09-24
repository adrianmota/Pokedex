using System.Collections.Generic;

namespace Application.ViewModels
{
    public class HomeViewModel
    {
        public List<PokemonViewModel> PokemonViewModel { get; set; }
        public List<RegionViewModel> Regions { get; set; }
        public string PokemonName { get; set; }
        public int RegionId { get; set; }
    }
}