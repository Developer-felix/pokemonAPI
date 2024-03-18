
using PocumanAPI.IRepository;
using PocumanAPI.Models; 
using System.Collections.Generic;
using System.Linq;
using PocumanAPI.Data;

namespace PocumanAPI.Repository
{
    public class PokemonRepository : IPokemanRepository
    {
        private readonly ApplicationDBContext _db;
        public PokemonRepository(ApplicationDBContext db )
        {
            this._db = db;
        }

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon)
        {
            var pokemonOwnerEntity = _db.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
            var category = _db.Category.Where(a => a.Id ==categoryId).FirstOrDefault();
            var pokemonOwner = new PokemonOwner()
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon
            };

            _db.Add(pokemonOwner);

            var pokemonCategory = new PokemonCategory()
            {
                Category = category,
                Pokemon = pokemon
            };

            _db.Add(pokemonCategory);

            _db.Add(pokemon);

            return Save();
        }

        public Pokemon GetPokemon(int id)
        {
            return _db.Pokemons.Where(p => p.Id == id).FirstOrDefault();
        }

        public Pokemon GetPokemon(string name)
        {
            return _db.Pokemons.Where(p => p.Name == name).FirstOrDefault();
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var review = _db.Reviews.Where(r => r.Pokemon.Id == pokeId);

            if (review.Count() <= 0)
            {
                return 0;
            }
            var rating = ((decimal)review.Sum(r => r.Rating) / review.Count());

            return rating;
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return  _db.Pokemons.OrderBy(p => p.Id).ToList();
        }

        public bool PokemonExists(int pokeId)
        {
            return _db.Pokemons.Any(p => p.Id == pokeId);
        }

        public bool Save()
        {
           var saved  = _db.SaveChanges();
           return saved > 0 ? true : false;
        }
    }
}
