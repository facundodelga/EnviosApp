using EnviosApp.Models;

namespace EnviosApp.Repository.Implementation {
    public interface IProviderRepository {
        IEnumerable<Provider> GetProvidersWithZones();
    }
}