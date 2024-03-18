using System.ComponentModel.DataAnnotations.Schema;

namespace PocumanAPI.Models
{
    public class PokemonOwner
    {
        public int PokemonId { get; set; }

        [ForeignKey(nameof(PokemonId))]
        public Pokemon Pokemon { get; set; }
        public int OwnerId {  get; set; }

        [ForeignKey(nameof(OwnerId))]
        public Owner Owner { get; set; }
    }
}
