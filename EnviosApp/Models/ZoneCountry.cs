namespace EnviosApp.Models
{

    public class ZoneCountry
    {
        public int ZoneId { get; set; } // FK a Zone
        public int CountryId { get; set; } // FK a Country

        // Relaciones de navegación
        public Zone Zone { get; set; }
        public Country Country { get; set; }
    }

}
