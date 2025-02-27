namespace EnviosApp.Models.DTOs.UpdateProvider {
    public class UpdateServiceTypeDTO {
        public long Id { get; set; }
        public string Name { get; set; } // Ejemplo: Económico, Prioritario
        public decimal PriceMultiplier { get; set; } // Multiplicador para el precio base
    }
}
