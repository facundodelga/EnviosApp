namespace EnviosApp.Models {
    public class Waybill {
        public long Id { get; set; }
        public string WaybillNumber { get; set; }
        public string Address { get; set; }
        public string? Description { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; }
        public decimal Weight { get; set; }
        public decimal Width { get; set; } = 0.1m;
        public decimal Height { get; set; } = 0.1m;
        public decimal Depth { get; set; } = 0.1m;
        public decimal Price { get; set; }
        public long ClientId { get; set; } // FK a Client
        public int ServiceTypeId { get; set; } // FK a ServiceType
        public int ProviderId { get; set; } // FK a Provider
        public int CountryId { get; set; } // FK a Country

        public ServiceType ServiceType { get; set; }
        public Provider Provider { get; set; }
        public Country Country { get; set; }
        public Client Client { get; set; }
        

        public Waybill() { }

    }
}
