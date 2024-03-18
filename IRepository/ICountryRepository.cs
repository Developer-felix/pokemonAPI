using PocumanAPI.Models;

namespace PocumanAPI.IRepository
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();

        Country GetCountry(int id); 

        Country GetCountryByOrner(int ownerId);

        ICollection<Owner> GetOwnersFromACountry(int countryId);

        bool CountryExists(int id);
    }
}
