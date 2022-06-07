using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database.Models;

namespace Database
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Pokemon> Pokemon { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<PokemonType> PokemonTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region tables
            modelBuilder.Entity<Pokemon>()
                        .ToTable("Pokemon");

            modelBuilder.Entity<Region>()
                        .ToTable("Regions");

            modelBuilder.Entity<PokemonType>()
                        .ToTable("PokemonType");
            #endregion

            #region "Primary keys"
            modelBuilder.Entity<Pokemon>()
                        .HasKey(pokemon => pokemon.Id);

            modelBuilder.Entity<Region>()
                        .HasKey(region => region.Id);

            modelBuilder.Entity<PokemonType>()
                        .HasKey(pokemonType => pokemonType.Id);
            #endregion

            #region Relationships
            modelBuilder.Entity<Region>()
                        .HasMany<Pokemon>(region => region.Pokemon)
                        .WithOne(pokemon => pokemon.Region)
                        .HasForeignKey(pokemon => pokemon.RegionId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PokemonType>()
                        .HasMany<Pokemon>(pokemonType => pokemonType.Pokemon)
                        .WithOne(pokemon => pokemon.PrimaryType)
                        .HasForeignKey(pokemon => pokemon.PrimaryTypeId)
                        .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region "Properties configuration"

            #region Pokemon
            modelBuilder.Entity<Pokemon>()
                        .Property(pokemon => pokemon.Name)
                        .IsRequired();

            modelBuilder.Entity<Pokemon>()
                        .Property(pokemon => pokemon.ImageUrl)
                        .IsRequired();
            #endregion

            #region Region
            modelBuilder.Entity<Region>()
                        .Property(region => region.Name)
                        .IsRequired();
            #endregion

            #region PokemonType
            modelBuilder.Entity<PokemonType>()
                        .Property(pokemonType => pokemonType.Name)
                        .IsRequired();
            #endregion

            #endregion
        }
    }
}
