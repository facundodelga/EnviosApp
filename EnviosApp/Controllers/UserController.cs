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
                    return StatusCode(500, "User not found");
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
                    return StatusCode(500, "User not found");
                }

                return Ok(UserDTO.UserToUserDTO(user.ToList()));
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = "adminOnly")]
        public IActionResult Post([FromBody] NewUserDTO userDTO) {
            try {
                var user = _userRepository.FindByUserName(userDTO.Username);

                if (user != null) {
                    return Forbid("Username already exists!");
                }

                Client newUser = new Client { 
                    Name =  userDTO.Name ,
                    UserName = userDTO.Username,
                    Password = userDTO.Password,
                    Role = userDTO.Role
                };

                _userRepository.Save(newUser);

                return Ok(userDTO);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "adminOnly")]
        public IActionResult Delete(int id) {
            try {
                var user = _userRepository.FindById(id);

                if (user == null) {
                    return StatusCode(500, "User not Found");
                }

                _userRepository.RemoveUser(user);

                return Ok(new UserDTO(user));
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        [Authorize(Policy = "adminOnly")]
        public IActionResult Update(long id, [FromBody] NewUserDTO userDto) {
            try {
                // Validar el DTO recibido
                if (userDto == null) {
                    return BadRequest("Datos del usuario son requeridos.");
                }

                // Buscar el usuario existente en la base de datos
                var user = _userRepository.FindById(id);
                if (user == null) {
                    return  StatusCode(500, "User not found");
                }

                // Actualizar las propiedades del usuario
                user.Name = userDto.Name;
                user.UserName = userDto.Username;
                user.Password = userDto.Password;
                user.Role = userDto.Role;

                // Guardar los cambios
                _userRepository.UpdateUser(user);

                return Ok(new UserDTO(user)); // Devuelve el usuario actualizado
            }
            catch (Exception ex) {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


    }
}
