namespace EnviosApp.Models.DTOs {
    public class ZoneDTO {
        public long Id { get; set; }
        public string Name { get; set; } // Ejemplo: Zona 1, Zona A
        public decimal BasePrice { get; set; } // Precio base

        public List<string> Countries { get; set; } = new List<string>();   

        public ZoneDTO(Zone zone) {
            Id = zone.Id;
            Name = zone.Name;
            BasePrice = zone.BasePrice;

            foreach (var zonecountry in zone.ZoneCountries) { 
                Countries.Add(zonecountry.Country.Name);
            }
        }
    }
}