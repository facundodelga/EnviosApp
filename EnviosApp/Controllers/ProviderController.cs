using EnviosApp.Models;
using EnviosApp.Models.DTOs.UpdateProvider;
using EnviosApp.Repository;
using EnviosApp.Repository.Implementation;
using EnviosApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnviosApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase {
        private IProviderService _providerService;
        

        public ProviderController(IProviderService providerService) {
            _providerService = providerService;
            
        }

        [HttpGet]
        [Authorize(Policy = "adminOnly")]
        public IActionResult GetAllProviders() {
            var result = _providerService.getAllProviders();
            if (result.IsSuccess == true) { 
                return Ok(result.Value);
            }

            return NotFound(result.Error);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "adminOnly")]
        public IActionResult GetProvider(long id) {
            var result = _providerService.getProviderById(id);

            if (result.IsSuccess == true) {
                return Ok(result.Value);
            }

            return NotFound(result.Error);
        }

        [HttpPost]
        [Authorize(Policy = "adminOnly")]
        public IActionResult CreateProvider([FromBody] CreateProviderDto dto) {
            var result = _providerService.addProvider(dto);
            if(!result.IsSuccess)
                return BadRequest(result.Error);
           
            return Ok(result.Value);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProvider(long id, [FromBody] UpdateProviderDTO dto) {
            var result = _providerService.updateProvider(id, dto);
            
            if(!result.IsSuccess) { return BadRequest(result.Error); }

            return NoContent();
        }

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
