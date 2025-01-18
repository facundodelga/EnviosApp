namespace EnviosApp.Models.DTOs {
    public class ZoneCountryDTO {
        
        public string Name { get; set; }

        public ZoneCountryDTO(ZoneCountry zoneCountry) {
            
            Name = zoneCountry.Country.Name;
        }
    }
}
