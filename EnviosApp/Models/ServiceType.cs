namespace EnviosApp.Models {
    public class ServiceType {
        public int Id { get; set; }
        public string Name { get; set; } // Ejemplo: Económico, Prioritario
        public decimal PriceMultiplier { get; set; } // Multiplicador para el precio base
        public long ProviderId {  get; set; }
        public Provider Provider { get; set; }
    }
}
