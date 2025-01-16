using EnviosApp.Models;
using EnviosApp.Repository.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnviosApp.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase {
        private IProviderRepository _providerRepository;

        public ProviderController(IProviderRepository providerRepository) {
            _providerRepository = providerRepository;
        }

        [HttpGet]
        public IActionResult GetAllProviders(long id) {
            var providers = _providerRepository.GetProvidersWithZones();

            if (providers == null) {
                return NotFound();
            }

            return Ok(providers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Provider>> GetProvider(long id) {
            var provider = _providerRepository.GetProviderById(id);

            if (provider == null) {
                return NotFound();
            }

            return Ok(provider);
        }

        [HttpPost]
        public IActionResult CreateProvider(CreateProviderDto dto) {
            var provider = new Provider {
                Name = dto.Name,
                Zones = new List<Zone>(),
                ServiceTypes = new List<ServiceType>()
            };

            _context.Providers.Add(provider);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProvider), new { id = provider.Id }, provider);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProvider(long id, UpdateProviderDto dto) {
            var provider = await _context.Providers.FindAsync(id);

            if (provider == null) {
                return NotFound();
            }

            provider.Name = dto.Name;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvider(long id) {
            var provider = await _context.Providers.FindAsync(id);

            if (provider == null) {
                return NotFound();
            }

            _context.Providers.Remove(provider);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
