using EnviosApp.Models;

namespace EnviosApp.Repository.Implementation {
    public interface IZoneCountryRepository {
        Zone FindByProviderCountry(long providerId, long countryId);
        void Save(ZoneCountry country);
    }
}