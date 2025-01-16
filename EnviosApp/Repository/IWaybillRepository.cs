using EnviosApp.Models;

namespace EnviosApp.Repository.Implementation {
    public interface IWaybillRepository {
        IEnumerable<Waybill> GetAllWaybills();
        IEnumerable<Waybill> GetWaybillsByClient(int clientId);
        IEnumerable<Waybill> GetWaybillsByProvider(int providerId);
    }
}