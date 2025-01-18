using EnviosApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnviosApp.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase {

        private ICountryRepository _countryRepository;

        public CountriesController(ICountryRepository countryRepository) {
            _countryRepository = countryRepository;
        }

        [HttpGet]
        public IActionResult GetAllCountries() { 
            var countries = _countryRepository.GetCountries();

            if (countries == null) {
                return NotFound();
            }
            return Ok(countries);
        }
    }
}
