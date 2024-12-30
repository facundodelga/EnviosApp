namespace EnviosApp.Models.DTOs
{
    public class NewUserDTO
    {
        public string Name { get; set; }
        public string Username{ get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public NewUserDTO(Client user) {
            Name = user.Name;
            Username = user.UserName;
            Password = user.Password;
            Role = user.Role;
        }

        public NewUserDTO() {
        }

        public static List<NewUserDTO> UserToNewUserDTO(List<Client> users) {
            List<NewUserDTO> userDTOs = new List<NewUserDTO>();

            foreach (Client user in users) { 
                userDTOs.Add(new NewUserDTO(user));
            }

            return userDTOs;
        }
    }
}