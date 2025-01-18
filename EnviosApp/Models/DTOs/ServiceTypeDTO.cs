namespace EnviosApp.Models.DTOs {
    public class ServiceTypeDTO {
        public long Id { get; set; }
        public string Name { get; set; } // Ejemplo: Económico, Prioritario
        public decimal PriceMultiplier { get; set; } // Multiplicador para el precio base

        public ServiceTypeDTO(ServiceType serviceType) {
            Id = serviceType.Id;
            Name = serviceType.Name;
            PriceMultiplier = serviceType.PriceMultiplier;

        }
    }
}