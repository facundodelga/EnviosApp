using EnviosApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EnviosApp.Repository.Implementation
{
    public class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        public CountryRepository(EnviosDBContext context) : base(context) { }

        public Country GetCountryByName(string name) {
            return FindByCondition(c => c.Name == name).FirstOrDefault();    
        }

        public Country GetCountryByAlpha(string name) {
            return FindByCondition(c => c.Name == name).FirstOrDefault();
        }

        public IEnumerable<Country> GetCountries() {
            return FindAll().ToList();
        }



        //public IEnumerable<Country> GetCountriesByZone(int zoneId)
        //{
        //    return FindByCondition(country => country.ZoneCountries.Any(zc => zc.ZoneId == zoneId)).ToList();
        //}

    }
}
