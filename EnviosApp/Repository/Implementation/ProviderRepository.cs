using EnviosApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EnviosApp.Repository.Implementation {
    public class ProviderRepository : RepositoryBase<Provider> {
        public ProviderRepository(DbContext context) : base(context) { }

        public IEnumerable<Provider> GetProvidersWithZones() {
            return FindAll().ToList();
        }
    }
}
