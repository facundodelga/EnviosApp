using EnviosApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EnviosApp.Repository.Implementation
{
    public class ZoneRepository : RepositoryBase<Zone>, IZoneRepository {
        public ZoneRepository(EnviosDBContext context) : base(context) { }

        public IEnumerable<Zone> GetZonesByProvider(int providerId) {
            return FindByCondition(z => z.ProviderId == providerId).AsEnumerable();
        }

        public void Save(Zone newZone) {
            Create(newZone);
            SaveChanges();  
        }
    }
}
