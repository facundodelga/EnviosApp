using EnviosApp.Models;

namespace EnviosApp.Repository
{
    public interface ICountryRepository
    {
        //IEnumerable<Country> GetCountriesByZone(int zoneId);
        Country GetCountryByName(string name);
        Country GetCountryByAlpha(string name);
        IEnumerable<Country> GetCountries();
        IEnumerable<Country> SearchByName(string search);
    }
}