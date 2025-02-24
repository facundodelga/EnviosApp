using EnviosApp.Models.DTOs;
using EnviosApp.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO) {
            string jwtKey = "jwt-key-delga-DESKTOP-2EUSJCE-Warning";


            if (loginDTO.Username.IsNullOrEmpty() || loginDTO.Password.IsNullOrEmpty()) {
                return BadRequest("null parameters");
            }
            try {
                var user = _userRepository.FindByUserName(loginDTO.Username);

                if (user == null || user.Password != loginDTO.Password)
                    return BadRequest("User notFound");


                var claims = new List<Claim>();

                //Me fijo si soy admin
                if (user.Role.Equals("admin")) {
                    claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, "admin"));
                }
                else {
                    claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, "user"));
                }
                claims.Add(new Claim("username", loginDTO.Username));
                claims.Add(new Claim("name", user.Name));

                // Antes de crear el token
                foreach (var claim in claims) {
                    Console.WriteLine($"Claim a incluir en token - Type: {claim.Type}, Value: {claim.Value}");
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var byteKey = Encoding.UTF8.GetBytes(jwtKey);

                var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);

                var tokenDescriptor = new SecurityTokenDescriptor() {

                    Subject = claimsIdentity,
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(byteKey), SecurityAlgorithms.HmacSha256Signature),
                };

                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
                var tokenjwt = tokenHandler.WriteToken(tokenConfig);
                //devuelvo el token para almacenarlo en el navegador
                return Ok(new { message = "Inicio de sesión exitoso", token = tokenjwt });
            }
            catch (Exception ex) {
            
                Console.WriteLine($"Exception : ");
                return BadRequest();
            
            }
            
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
