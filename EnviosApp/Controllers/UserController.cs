using EnviosApp.Models;
using EnviosApp.Models.DTOs;
using EnviosApp.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EnviosApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {
        private IUserRepository _userRepository;

        public UserController(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "adminOnly")]
        public IActionResult GetById(long id) {
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

        [HttpGet("all")]
        [Authorize(Policy = "adminOnly")]
        public IActionResult GetAll() {
            try {
                var user = _userRepository.GetAllUsers();

                if (user == null) {
                    return Forbid();
                }

                return Ok(UserDTO.UserToUserDTO(user.ToList()));
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = "adminOnly")]
        public IActionResult Post([FromBody] UserDTO userDTO) {
            try {
                var user = _userRepository.FindByUserName(userDTO.Username);

                if (user != null) {
                    return Forbid("Username already exists!");
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
