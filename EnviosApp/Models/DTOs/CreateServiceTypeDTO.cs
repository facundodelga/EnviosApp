namespace EnviosApp.Models.DTOs {
    public class CreateServiceTypeDTO {
        public string Name { get; set; } // Ejemplo: Económico, Prioritario
        public decimal PriceMultiplier { get; set; } // Multiplicador para el precio base
    }
}
