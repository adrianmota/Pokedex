using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class PokemonTypeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "* Debes de especificar el nombre")]
        public string Name { get; set; }
    }
}
