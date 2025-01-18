using EnviosApp.Models;

namespace EnviosApp.Repository
{
    public interface IZoneRepository
    {
        IEnumerable<Zone> GetZonesByProvider(long providerId);
        Zone GetZoneByProviderAndName(long providerId, string name);
        void Save(Zone newZone);
    }
}