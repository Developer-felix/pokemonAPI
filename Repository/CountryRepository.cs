using AutoMapper;
using PocumanAPI.Data;
using PocumanAPI.IRepository;
using PocumanAPI.Models;

namespace PocumanAPI.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDBContext _db;
        private readonly IMapper _mapper;

        public CountryRepository(ApplicationDBContext db, IMapper mapper)
        {
            this._db = db;
            this._mapper = mapper; 
        }

        public bool CountryExists(int id)
        {
            return _db.Countrys.Any(c => c.Id == id);
        }

        public ICollection<Country> GetCountries()
        {
            return _db.Countrys.OrderBy(c => c.Id).ToList();
        }

        public Country GetCountry(int id)
        {
            return _db.Countrys.Where(c => c.Id == id).FirstOrDefault();
        }

        public Country GetCountryByOrner(int ownerId)
        {
            return _db.Owners.Where(o => o.Id == ownerId).Select(c=>c.Country).FirstOrDefault();
        }

        public ICollection<Owner> GetCountryOwners()
        {
            throw new NotImplementedException();
        }

        public ICollection<Owner> GetOwnersFromACountry(int countryId)
        {
            return _db.Owners.Where(o => o.Country.Id == countryId).ToList();
        }
    }
}
