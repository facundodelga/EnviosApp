namespace EnviosApp.Models
{
    public class Zone
    {
        public long Id { get; set; }
        public long ProviderId { get; set; } // FK a Provider
        public string Name { get; set; } // Ejemplo: Zona 1, Zona A
        public decimal BasePrice { get; set; } // Precio base
        // Relación muchos a muchos con Zone
        public ICollection<ZoneCountry> ZoneCountries { get; set; }

        public Provider Provider { get; set; }

        public override bool Equals(object? obj) {
            return obj is Zone zone &&
                   Id == zone.Id &&
                   Name == zone.Name;
        }
    }
}
