using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
