namespace EnviosApp.Models.DTOs {
    public class CreateZoneDTO {
        public string Name { get; set; } // Ejemplo: Zona 1, Zona A
        public decimal BasePrice { get; set; } // Precio base
        public List<string> Countries { get; set; }
    }
}
