namespace EnviosApp.Models
{
    public class Zone
    {
        public int Id { get; set; }
        public int ProviderId { get; set; } // FK a Provider
        public string Name { get; set; } // Ejemplo: Zona 1, Zona A
        public decimal BasePrice { get; set; } // Precio base
        // Relación muchos a muchos con Zone
        public ICollection<ZoneCountry> ZoneCountries { get; set; }

        public Provider Provider { get; set; }
    }
}
