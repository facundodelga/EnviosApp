namespace EnviosApp.Models {
    public class Client {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Organization { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public ICollection<Waybill>? Waybills { get; set; } = new List<Waybill>();

    }
}
