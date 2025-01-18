using EnviosApp.Models.DTOs;

namespace EnviosApp.Controllers {
    public class CreateProviderDto {
        public string Name { get; set; }
        public ICollection<CreateZoneDTO> Zones { get; set; }
        public ICollection<CreateServiceTypeDTO> ServiceTypes { get; set; }

    }
}