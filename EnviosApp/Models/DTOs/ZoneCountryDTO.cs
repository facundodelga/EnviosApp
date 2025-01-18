namespace EnviosApp.Models.DTOs {
    public class ZoneCountryDTO {
        public long Id {  get; set; }
        public string Name { get; set; }
        public string Alpha { get; set; }

        public ZoneCountryDTO(ZoneCountry zoneCountry) {
            Id  = zoneCountry.CountryId;
            Name = zoneCountry.Country.Name;
            Alpha = zoneCountry.Country.Alpha;
        }
    }
}
