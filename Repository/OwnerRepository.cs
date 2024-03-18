using PocumanAPI.Data;
using PocumanAPI.IRepository;
using PocumanAPI.Models;

namespace PocumanAPI.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly ApplicationDBContext _db;
        public OwnerRepository(ApplicationDBContext db) {
            this._db = db;
        }
        public Owner GetOwner(int ownerId)
        {
            return _db.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerOfAPokemon(int owner)
        {
            return _db.PokemonOwners.Where(p => p.Pokemon.Id == owner).Select(o => o.Owner).ToList();
        }

        public ICollection<Owner> GetOwners()
        {
            return _db.Owners.OrderBy(o => o.Id).ToList();
        }

        public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
        {
            return _db.PokemonOwners.Where(p => p.Owner.Id == ownerId).Select(p => p.Pokemon).ToList();
        }

        public bool OwnerExists(int ownerId)
        {
            return _db.Owners.Any(o => o.Id == ownerId);
        }
    }
}
