namespace Application.ViewModels
{
    public class PokemonViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public PokemonTypeViewModel PrimaryType { get; set; }
        public PokemonTypeViewModel SecondaryType { get; set; }
        public RegionViewModel Region { get; set; }
    }
}