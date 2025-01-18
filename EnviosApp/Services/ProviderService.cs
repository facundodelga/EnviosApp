using EnviosApp.Repository.Implementation;
using EnviosApp.Repository;

namespace EnviosApp.Services {
    public class ProviderService {
        private IProviderRepository _providerRepository;
        private IServiceTypeRepository _serviceTypeRepository;
        private IZoneRepository _zoneRepository;

        public ProviderService(
            IProviderRepository providerRepository,
            IZoneRepository zoneRepository,
            IServiceTypeRepository serviceTypeRepository) {
            _providerRepository = providerRepository;
            _serviceTypeRepository = serviceTypeRepository;
            _zoneRepository = zoneRepository;
        }


    }
}
