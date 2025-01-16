using EnviosApp.Models;

namespace EnviosApp.Repository.Implementation {
    public class ZoneCountryRepository : RepositoryBase<ZoneCountry>, IZoneCountryRepository {
        public ZoneCountryRepository(EnviosDBContext context) : base(context) { }

        public Zone FindByProviderCountry(long providerId, long countryId) {
            return FindByCondition(zc => zc.CountryId == countryId && zc.Zone.ProviderId == providerId).Select(zc => zc.Zone).FirstOrDefault();
        }
    }

}
