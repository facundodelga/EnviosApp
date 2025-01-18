using EnviosApp.Repository.Implementation;
using EnviosApp.Repository;
using EnviosApp.Models;
using EnviosApp.Models.DTOs;
using EnviosApp.Controllers;
using Humanizer;

namespace EnviosApp.Services {
    public class ProviderService : IProviderService {
        private IProviderRepository _providerRepository;
        private IServiceTypeRepository _serviceTypeRepository;
        private IZoneRepository _zoneRepository;
        private IZoneCountryRepository _zoneCountryRepository;
        private ICountryRepository _countryRepository;

        public ProviderService(
            IProviderRepository providerRepository,
            IZoneRepository zoneRepository,
            IServiceTypeRepository serviceTypeRepository, 
            IZoneCountryRepository zoneCountryRepository,
            ICountryRepository countryRepository) {
            _providerRepository = providerRepository;
            _serviceTypeRepository = serviceTypeRepository;
            _zoneRepository = zoneRepository;
            _countryRepository = countryRepository;
            _zoneCountryRepository = zoneCountryRepository;
        }

        public Result<List<ProviderDTO>> getAllProviders() {
            var providers = _providerRepository.GetProvidersWithZones();

            if (providers == null) {
                return Result<List<ProviderDTO>>.Failure("Providers not found");
            }

            List<ProviderDTO> providerDTOs = new List<ProviderDTO>();

            foreach (var provider in providers) {
                providerDTOs.Add(new ProviderDTO(provider));
            }

            return Result<List<ProviderDTO>>.Success(providerDTOs);
        }

        public Result<ProviderDTO> getProviderByName(string name) {
            var provider = _providerRepository.GetProvider(name);

            if (provider == null) {
                return Result<ProviderDTO>.Failure("Provider with name: "+name+ " not found");
            }

            return Result<ProviderDTO>.Success(new ProviderDTO(provider));
        }

        public Result<ProviderDTO> getProviderById(long id) {
            var provider = _providerRepository.GetProviderById(id);

            if (provider == null) {
                return Result<ProviderDTO>.Failure("Provider with id: " + id + " not found");
            }

            return Result<ProviderDTO>.Success(new ProviderDTO(provider));
        }

        public Result<string> addProvider(CreateProviderDto dto) {
            var provider = _providerRepository.GetProvider(dto.Name);
            if (provider != null) {
                return Result<string>.Failure ( "Provider already exists");
            }

            bool banderaRepetido = false;
            foreach (var zone in dto.Zones) {
                if (provider.Zones.Select(z => z.Name.Equals(zone.Name)).Any()) {
                    banderaRepetido = true;
                }
            }
            if (banderaRepetido) {
                return Result<string>.Failure("Zone on Provider already exists");
            }

            foreach (var service in dto.ServiceTypes) {
                if (provider.ServiceTypes.Select(z => z.Name.Equals(service.Name)).Any()) {
                    banderaRepetido = true;
                }
            }
            if (banderaRepetido) {
                return Result<string>.Failure("ServiceType on Provider already exists");
            }

            var newProvider = new Provider { Name = dto.Name };

            _providerRepository.Save(newProvider);
            var aux = _providerRepository.GetProvider(newProvider.Name);

            foreach (var zone in dto.Zones) {
                var newZone = new Zone { Name = zone.Name, BasePrice = zone.BasePrice, ProviderId = aux.Id};
                _zoneRepository.Save(newZone);
            }

            //Para cada zona voy a asignarle los paises para eso necesito los nombres de cada pais y linkearlo con ZoneCountry
            foreach (var zone in dto.Zones) {
                
                var zoneRepo = _zoneRepository.GetZoneByProviderAndName(aux.Id, zone.Name);
                if (zoneRepo != null) {
                    foreach (var country in zone.Countries) {
                        var countryRepo = _countryRepository.GetCountryByName(country);
                        ZoneCountry zoneCountry = new ZoneCountry { CountryId = countryRepo.Id , ZoneId = zoneRepo.Id };

                        _zoneCountryRepository.Save(zoneCountry);    
                    }
                    
                }
            }

            foreach (var service in dto.ServiceTypes) {
                var newservice = new ServiceType { Name = service.Name, PriceMultiplier = service.PriceMultiplier, ProviderId = aux.Id };
                _serviceTypeRepository.Save(newservice);
            }


            return Result<string>.Success("Provider created");
        }

    }
}
