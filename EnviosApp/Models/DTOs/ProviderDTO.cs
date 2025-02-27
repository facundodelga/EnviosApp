using Newtonsoft.Json;

namespace EnviosApp.Models.DTOs {
    public class ProviderDTO {
        
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("serviceTypes")]
        public List<ServiceTypeDTO> ServiceTypes { get; set; } = new List<ServiceTypeDTO>();

        [JsonProperty("zones")]
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
