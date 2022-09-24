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

        [Required(ErrorMessage = "* Debes especificar el nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "* Debes especificar la ruta de la imagen")]
        public string ImageUrl { get; set; }

        [Range(1, 1000, ErrorMessage = "* Debes especificar el tipo primario")]
        public int PrimaryTypeId { get; set; }
        public int SecondaryTypeId { get; set; }

        [Range(1, 1000, ErrorMessage = "* Debes seleccionar la región")]
        public int RegionId { get; set; }
        public List<RegionViewModel> Regions { get; set; }
        public List<PokemonTypeViewModel> PokemonTypes { get; set; }
    }
}