using Newtonsoft.Json;

namespace EnviosApp.Models.DTOs.UpdateProvider
{
    public class UpdateProviderDTO
    {
        
        public long Id { get; set; }

        
        public string Name { get; set; }

        
        public List<UpdateServiceTypeDTO> ServiceTypes { get; set; } = new List<UpdateServiceTypeDTO>();

        
        public List<UpdateZoneDTO> Zones { get; set; } = new List<UpdateZoneDTO>();


    }
}
