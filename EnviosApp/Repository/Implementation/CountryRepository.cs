using EnviosApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EnviosApp.Repository.Implementation
{
    public class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        public CountryRepository(DbContext context) : base(context) { }

        public IEnumerable<Country> GetCountriesByZone(int zoneId)
        {
            return FindByCondition(country => country.ZoneCountries.Any(zc => zc.ZoneId == zoneId)).ToList();
        }

    }
}
