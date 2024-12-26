using EnviosApp.Models;
using EnviosApp.Models.DTOs;
using EnviosApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnviosApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {
        private IUserRepository _userRepository;

        public UserController(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Get(long id) {
            try {
                var user = _userRepository.FindById(id);

                if (user == null) { 
                    return Forbid();
                }

                return Ok(user);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserDTO userDTO) {
            try {
                var user = _userRepository.FindByUserName(userDTO.Username);

                if (user != null) {
                    return Forbid();
                }

                User newUser = new User { 
                    Name =  userDTO.Name ,
                    UserName = userDTO.Username,
                    Password = userDTO.Password
                
                };

                _userRepository.Save(newUser);

                return Ok(userDTO);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
