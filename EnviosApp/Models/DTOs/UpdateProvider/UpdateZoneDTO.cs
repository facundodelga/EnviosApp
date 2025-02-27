namespace EnviosApp.Models.DTOs.UpdateProvider {
    public class UpdateZoneDTO {
        public long Id { get; set; }
        public string Name { get; set; } // Ejemplo: Zona 1, Zona A
        public decimal BasePrice { get; set; } // Precio base

        public List<UpdateZoneCountryDTO> Countries { get; set; } = new List<UpdateZoneCountryDTO>();
    }
}
