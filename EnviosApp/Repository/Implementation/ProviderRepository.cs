using EnviosApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EnviosApp.Repository.Implementation {
    public class ProviderRepository : RepositoryBase<Provider>, IProviderRepository {
        public ProviderRepository(EnviosDBContext context) : base(context) { }

        public Provider GetProvider(string name) {
            return FindByCondition(p => p.Name.ToLower().Equals(name))
                .Include(p => p.ServiceTypes)
                .Include(p => p.Zones).ThenInclude(z => z.ZoneCountries).ThenInclude(zc => zc.Country)
                .FirstOrDefault();
        }

        public Provider GetProviderOnly(string name) {
            return FindByCondition(p => p.Name.ToLower().Equals(name))
                .FirstOrDefault();
        }

        public Provider GetProviderNoCountry(string name) {
            return FindByCondition(p => p.Name.ToLower().Equals(name))
                .Include(p => p.ServiceTypes)
                .Include(p => p.Zones).ThenInclude(z => z.ZoneCountries).FirstOrDefault();
        }

        public Provider GetProviderById(long id) {
            return FindByCondition(p => p.Id == id)
                .Include(p => p.ServiceTypes)
                .Include(p => p.Zones).ThenInclude(z => z.ZoneCountries).ThenInclude(zc => zc.Country).AsNoTracking()
                .FirstOrDefault();
        }
        public Provider GetProviderByIdNoCountry(long id) {
            return FindByCondition(p => p.Id == id)
                .Include(p => p.ServiceTypes)
                .Include(p => p.Zones).ThenInclude(z => z.ZoneCountries).AsNoTracking()
                .FirstOrDefault();
        }

        public IEnumerable<Provider> GetProvidersWithZones() {
            return FindAll()
                .Include(p => p.ServiceTypes)
                .Include(p => p.Zones).ThenInclude(z => z.ZoneCountries).ThenInclude(zc => zc.Country)
                .ToList();
        }

        public void Save(Provider provider) {
            Create(provider);
            SaveChanges();
        }

        public void Update(Provider provider) {
            Update(provider);
            SaveChanges();

        }

        public void Remove(Provider provider) {
            Delete(provider);
            SaveChanges();
        }
    }
}
