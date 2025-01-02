namespace EnviosApp.Models {
    public class Waybill {
        public string Id { get; set; }
        public string WaybillNumber { get; set; }
        public string Service { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public float Weight { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Depth {  get; set; }
        public float Price {  get; set; }
        public Client Client { get; set; }

        public Waybill() { }

    }
}
