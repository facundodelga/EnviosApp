using EnviosApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EnviosApp.Repository.Implementation
{
    public class ZoneRepository : RepositoryBase<Zone>, IZoneRepository {
        public ZoneRepository(EnviosDBContext context) : base(context) { }

        public Zone GetZoneByProviderAndName(long providerId, string name) {
            return FindByCondition(z => z.ProviderId == providerId && z.Name.Equals(name))
                .Include(z => z.ZoneCountries).
                ThenInclude(c => c.Country)
                .FirstOrDefault();
        }

        public IEnumerable<Zone> GetZonesByProvider(long providerId) {
            return FindByCondition(z => z.ProviderId == providerId).Include(z => z.ZoneCountries).
                ThenInclude(c => c.Country).AsEnumerable();
        }

        public Zone GetZoneById(long Id) { 
            return FindByCondition ( z => z.Id == Id).Include(z => z.ZoneCountries).FirstOrDefault();
        }

        public void Save(Zone newZone) {
            Create(newZone);
            SaveChanges();  
        }

        
    }
}
