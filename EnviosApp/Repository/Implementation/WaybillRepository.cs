using EnviosApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EnviosApp.Repository.Implementation {
    public class WaybillRepository : RepositoryBase<Waybill> {
        public WaybillRepository(DbContext context) : base(context) { }

        public IEnumerable<Waybill> GetAllWaybills() {
            return FindAll()
                .Include(w => w.Client)
                .Include(w => w.Provider)
                .Include(w => w.Country).ThenInclude(c => c.ZoneCountries)
                .Include(w => w.ServiceType)
                .AsEnumerable();
        }

        public IEnumerable<Waybill> GetWaybillsByProvider(int providerId) {
            return FindByCondition(w => w.ProviderId == providerId).Include(w => w.Client)
                .Include(w => w.Provider)
                .Include(w => w.Country).ThenInclude(c => c.ZoneCountries)
                .Include(w => w.ServiceType)
                .AsEnumerable();
        }

        public IEnumerable<Waybill> GetWaybillsByClient(int clientId) {
            return FindByCondition(w => w.ClientId == clientId).Include(w => w.Client)
                .Include(w => w.Provider)
                .Include(w => w.Country).ThenInclude(c => c.ZoneCountries)
                .Include(w => w.ServiceType)
                .AsEnumerable(); 
        }
    }
}
