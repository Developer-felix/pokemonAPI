using System.ComponentModel.DataAnnotations.Schema;

namespace PocumanAPI.Models
{
    public class PokemonCategory
    {
        public int PokemonId { get; set; }

        [ForeignKey(nameof(PokemonId))]
        public Pokemon Pokemon { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        public ICollection<PokemonCategory> PokemonCategories { get;}
    }
}
