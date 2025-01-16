namespace EnviosApp.Models
{
    public class Provider
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ICollection<Zone> Zones { get; set; }
        public ICollection<ServiceType> ServiceTypes { get; set; }
    }
}
