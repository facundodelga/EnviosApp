using EnviosApp.Models;
using EnviosApp.Models.DTOs;
using EnviosApp.Repository;
using EnviosApp.Repository.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnviosApp.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase {
        private IProviderRepository _providerRepository;
        private IServiceTypeRepository _serviceTypeRepository;
        private IZoneRepository _zoneRepository;

        public ProviderController(
            IProviderRepository providerRepository, 
            IZoneRepository zoneRepository, 
            IServiceTypeRepository serviceTypeRepository) {
            _providerRepository = providerRepository;
            _serviceTypeRepository = serviceTypeRepository;
            _zoneRepository = zoneRepository;
        }

        [HttpGet]
        [Authorize(Policy = "adminOnly")]
        public IActionResult GetAllProviders() {
            var providers = _providerRepository.GetProvidersWithZones();

            if (providers == null) {
                return NotFound();
            }
            
            List<ProviderDTO> providerDTOs = new List<ProviderDTO>();
            
            foreach (var provider in providers) {
                providerDTOs.Add(new ProviderDTO(provider));
            }

            return Ok(providerDTOs);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "adminOnly")]
        public IActionResult GetProvider(long id) {
            var provider = _providerRepository.GetProviderById(id);

            if (provider == null) {
                return NotFound();
            }

            return Ok(new ProviderDTO(provider));
        }

        [HttpPost]
        [Authorize(Policy = "adminOnly")]
        public IActionResult CreateProvider([FromBody] CreateProviderDto dto) {
            var provider = _providerRepository.GetProvider(dto.Name);
            if (provider != null) {
                return StatusCode(500, "Provider already exists");
            }

            bool banderaRepetido = false;  
            foreach (var zone in dto.Zones) {
                if (provider.Zones.Select(z => z.Name.Equals(zone.Name)).Any()) { 
                    banderaRepetido = true;    
                }
            }
            if (banderaRepetido) {
                return StatusCode(500, "Zone on Provider already exists");
            }

            foreach (var service in dto.ServiceTypes) {
                if (provider.ServiceTypes.Select(z => z.Name.Equals(service.Name)).Any()) {
                    banderaRepetido = true;
                }
            }
            if (banderaRepetido) {
                return StatusCode(500, "ServiceType on Provider already exists");
            }

            var newProvider = new Provider { Name = dto.Name };

            _providerRepository.Save(newProvider);
            var aux = _providerRepository.GetProvider(newProvider.Name);

            foreach (var zone in dto.Zones) {
                var newZone = new Zone { Name = zone.Name, BasePrice = zone.BasePrice , ProviderId = aux.Id};
                _zoneRepository.Save(newZone);
            }

            foreach (var service in dto.ServiceTypes) {
                var newservice = new ServiceType { Name = service.Name, PriceMultiplier = service.PriceMultiplier, ProviderId = aux.Id };
                _serviceTypeRepository.Save(newservice);
            }

            return Ok();
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateProvider(long id, UpdateProviderDto dto) {
        //    var provider = await _context.Providers.FindAsync(id);

        //    if (provider == null) {
        //        return NotFound();
        //    }

        //    provider.Name = dto.Name;
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProvider(long id) {
        //    var provider = await _context.Providers.FindAsync(id);

        //    if (provider == null) {
        //        return NotFound();
        //    }

        //    _context.Providers.Remove(provider);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}
