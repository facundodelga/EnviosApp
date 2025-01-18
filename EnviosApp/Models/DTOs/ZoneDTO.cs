namespace EnviosApp.Models.DTOs {
    public class ZoneDTO {
        public long Id { get; set; }
        public string Name { get; set; } // Ejemplo: Zona 1, Zona A
        public decimal BasePrice { get; set; } // Precio base

        public ZoneDTO(Zone zone) {
            Id = zone.Id;
            Name = zone.Name;
            BasePrice = zone.BasePrice;

        }
    }
}