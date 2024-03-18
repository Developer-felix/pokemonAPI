using PocumanAPI.Data;
using PocumanAPI.IRepository;
using PocumanAPI.Models;

namespace PocumanAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _db;
        public CategoryRepository(ApplicationDBContext db) { 
            _db = db;
        }

        public ICollection<Category> GetCategories()
        {
            return _db.Category.OrderBy(c => c.Id).ToList();
        }

        public Category GetCategory(int id)
        {
            return _db.Category.FirstOrDefault(c => c.Id == id);
        }

        public bool CategoryExists(int id)
        {
            return _db.Category.Any(c => c.Id == id);
        } 

        public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
        {
            var pokemons = _db.PokemonCategories.Where(pc => pc.Category.Id == categoryId).Select(pc => pc.Pokemon).ToList();

            return pokemons;
        }
    }
}
