using EnviosApp.Models.DTOs;
using EnviosApp.Models;
using EnviosApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Diagnostics.Metrics;
using System.Net;
using System.Reflection.Emit;

namespace EnviosApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase {

        private IClientRepository _ClientRepository;

        public ClientController(IClientRepository ClientRepository) {
            _ClientRepository = ClientRepository;
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "userOnly")]
        public IActionResult GetById(long id) {
            try {
                var client = _ClientRepository.FindById(id);

                if (client == null) {
                    return StatusCode(500, "Client not found");
                }

                return Ok(new ClientDTO(client));
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Policy = "userOnly")]
        public IActionResult GetAll() {
            try {
                var Client = _ClientRepository.GetAllClients();

                if (Client == null) {
                    return StatusCode(500, "Client not found");
                }

                return Ok(ClientDTO.ClientToClientDTO(Client.ToList()));
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = "userOnly")]
        public IActionResult Post([FromBody] ClientDTO clientDTO) {
            try {
                var client = _ClientRepository.FindByEmail(clientDTO.Email);

                if (client != null) {
                    return StatusCode(500, "Client already exists");
                }

                Client newClient = new Client {
                    Name = clientDTO.Name,
                    Organization = clientDTO.Organization,
                    Telephone = clientDTO.Telephone,
                    Email = clientDTO.Email,
                    Address = clientDTO.Address,
                    City = clientDTO.City,
                    ZipCode = clientDTO.ZipCode,
                    Country = clientDTO.Country
                };

                _ClientRepository.Save(newClient);

                return Ok(clientDTO);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "adminOnly")]
        public IActionResult Delete(int id) {
            try {
                var client = _ClientRepository.FindById(id);

                if (client == null) {
                    return StatusCode(500, "Client not Found");
                }

                _ClientRepository.RemoveClient(client);

                return Ok(new ClientDTO(client));
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        [Authorize(Policy = "adminOnly")]
        public IActionResult Update(long id, [FromBody] ClientDTO clientDTO) {
            try {
                // Validar el DTO recibido
                if (clientDTO == null) {
                    return BadRequest("Datos del usuario son requeridos.");
                }

                // Buscar el usuario existente en la base de datos
                var client = _ClientRepository.FindById(id);
                if (client == null) {
                    return StatusCode(500, "Client not found");
                }

                // Actualizar las propiedades del usuario
                client.Name = clientDTO.Name;
                client.Organization = clientDTO.Organization;
                client.Telephone = clientDTO.Telephone;
                client.Email = clientDTO.Email;
                client.Address = clientDTO.Address;
                client.City = clientDTO.City;
                client.ZipCode = clientDTO.ZipCode;
                client.Country = clientDTO.Country;

                // Guardar los cambios
                _ClientRepository.UpdateClient(client);

                return Ok(new ClientDTO(client)); // Devuelve el usuario actualizado
            }
            catch (Exception ex) {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

    }
}
