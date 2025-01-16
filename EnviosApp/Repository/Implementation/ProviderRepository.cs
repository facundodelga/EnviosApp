using EnviosApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EnviosApp.Repository.Implementation {
    public class ProviderRepository : RepositoryBase<Provider>, IProviderRepository {
        public ProviderRepository(EnviosDBContext context) : base(context) { }

        public IEnumerable<Provider> GetProvidersWithZones() {
            return FindAll().ToList();
        }
    }
}
