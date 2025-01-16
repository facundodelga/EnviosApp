namespace EnviosApp.Models
{

    public class ZoneCountry
    {
        public long Id { get; set; }
        public long ZoneId { get; set; } // FK a Zone
        public long CountryId { get; set; } // FK a Country

        // Relaciones de navegación
        public Zone Zone { get; set; }
        public Country Country { get; set; }
    }

}
