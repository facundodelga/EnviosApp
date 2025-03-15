using EnviosApp.Controllers;
using EnviosApp.Models;
using EnviosApp.Models.DTOs;
using EnviosApp.Models.DTOs.UpdateProvider;

namespace EnviosApp.Services
{
    public interface IProviderService {
        Result<string> addProvider(CreateProviderDto dto);
        Result<List<ProviderDTO>> getAllProviders();
        Result<ProviderDTO> getProviderById(long id);
        Result<ProviderDTO> getProviderByName(string name);
        Result<ProviderDTO> updateProvider(long id, UpdateProviderDTO dto);
    }
}