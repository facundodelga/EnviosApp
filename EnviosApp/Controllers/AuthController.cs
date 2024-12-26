using EnviosApp.Models.DTOs;
using EnviosApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EnviosApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO userDTO) {
            if (userDTO.Username.IsNullOrEmpty() || userDTO.Password.IsNullOrEmpty()) {
                return BadRequest();
            }
            var user = _userRepository.FindByUserName(userDTO.Username);
            if (user == null)
                return BadRequest();

            if (user.Password != userDTO.Password) { 
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout() {
            try {
                //await HttpContext.SignOutAsync(
                //CookieAuthenticationDefaults.AuthenticationScheme);
                return Ok();
            }
            catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
