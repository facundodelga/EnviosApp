using EnviosApp.Models;
using EnviosApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EnviosApp.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase {

        private ICountryRepository _countryRepository;

        public CountriesController(ICountryRepository countryRepository) {
            _countryRepository = countryRepository;
        }

        //[HttpGet("/all")]
        //[HttpGet]
        //public IActionResult GetAllCountries() { 
        //    var countries = _countryRepository.GetCountries();

        //    if (countries == null) {
        //        return NotFound();
        //    }
        //    return Ok(countries);
        //}

        [HttpGet]
        public IActionResult GetCountries([FromQuery] string? search) {
            // Filtrar países que coincidan con el término de búsqueda
            List<Country> countries = new List<Country>();
            
            if (search == null || search.Equals("")) {
                 countries = _countryRepository.GetCountries().ToList();
            }
            else {
                 countries = _countryRepository.SearchByName(search).ToList();
            }

            return Ok(countries);
        }
    }
}
