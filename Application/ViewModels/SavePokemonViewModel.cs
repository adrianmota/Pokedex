using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class SavePokemonViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe especificar el nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe especificar la ruta de la imagen")]
        public string ImageUrl { get; set; }

        [Range(1, 1000, ErrorMessage = "Debe especificar el tipo primario")]
        public int PrimaryTypeId { get; set; }
        public int SecondaryTypeId { get; set; }

        [Range(1, 1000, ErrorMessage = "Debe seleccionar la región")]
        public int RegionId { get; set; }
        public List<RegionViewModel> Regions { get; set; }
        public List<PokemonTypeViewModel> PokemonTypes { get; set; }
    }
}
