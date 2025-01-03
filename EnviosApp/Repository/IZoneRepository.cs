using EnviosApp.Models;

namespace EnviosApp.Repository
{
    public interface IZoneRepository
    {
        IEnumerable<Zone> GetZonesByProvider(int providerId);
    }
}