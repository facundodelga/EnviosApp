using EnviosApp.Models;

namespace EnviosApp.Repository.Implementation {
    public interface IProviderRepository {
        IEnumerable<Provider> GetProvidersWithZones();
        Provider GetProvider(string name);
        Provider GetProviderById(long id);

        void Save(Provider provider);
    }
}