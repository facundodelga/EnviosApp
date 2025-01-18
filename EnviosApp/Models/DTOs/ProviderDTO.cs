namespace EnviosApp.Models.DTOs {
    public class ProviderDTO {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<ZoneDTO> Zones { get; set; }
        public ICollection<ServiceTypeDTO> ServiceTypes { get; set; }

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
