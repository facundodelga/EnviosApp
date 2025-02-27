
namespace EnviosApp.Models
{
    public class Country
    {
        public long Id { get; set; }
        public string Alpha { get; set; }
        public string Name { get; set; }

        ////Relación muchos a muchos con Zone
        public ICollection<ZoneCountry> ZoneCountries { get; set; }

        public override bool Equals(object? obj) {
            return obj is Country country &&
                   Id == country.Id &&
                   Alpha == country.Alpha &&
                   Name == country.Name;
        }
    }
}
