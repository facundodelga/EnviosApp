namespace EnviosApp.Models.DTOs {
    public class ClientDTO {

        public long Id { get; set; }
        public string Name { get; set; }
        public string Organization { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }

        public ClientDTO(Client client) {
            Id = client.Id;
            Name = client.Name;
            Organization = client.Organization;
            Telephone = client.Telephone;
            Email = client.Email;
            Address = client.Address;
            City = client.City;
            ZipCode = client.ZipCode;
            Country = client.Country;
        }

        public ClientDTO(string name, string organization, string telephone, string email, string address, string city, string zipCode, string country) {
            Name = name;
            Organization = organization;
            Telephone = telephone;
            Email = email;
            Address = address;
            City = city;
            ZipCode = zipCode;
            Country = country;
        }

        public ClientDTO() {
        }

        public static List<ClientDTO> ClientToClientDTO(List<Client> users) {
            List<ClientDTO> userDTOs = new List<ClientDTO>();

            foreach (Client user in users) {
                userDTOs.Add(new ClientDTO(user));
            }

            return userDTOs;

        }
    }
}