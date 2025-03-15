using Newtonsoft.Json;

namespace EnviosApp.Models.DTOs {
    public class ProviderDTO {
        

        public long Id { get; set; }

    
        public string Name { get; set; }

        public List<ServiceTypeDTO> ServiceTypes { get; set; } = new List<ServiceTypeDTO>();

    
        public List<ZoneDTO> Zones { get; set; } = new List<ZoneDTO>();

       

        public ProviderDTO(Provider provider) {
            Id = provider.Id;
            Name = provider.Name;
            if (provider.Zones != null)
                Zones = provider.Zones.Select(z => new ZoneDTO(z)).ToList();
            else
                Zones = new List<ZoneDTO>();

            if (provider.ServiceTypes != null)
                ServiceTypes = provider.ServiceTypes.Select(st => new ServiceTypeDTO(st)).ToList();
            else
                ServiceTypes = new List<ServiceTypeDTO>();
        }

       
    }
}
