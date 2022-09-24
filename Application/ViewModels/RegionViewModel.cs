using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class RegionViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "* Debes especificar el nombre")]
        public string Name { get; set; }
    }
}