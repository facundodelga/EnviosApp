namespace EnviosApp.Models.DTOs
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Username{ get; set; }
        public string Password { get; set; }

        public UserDTO(User user) {
            Name = user.Name;
            Username = user.UserName;
            Password = user.Password;
        }

        public UserDTO() {
        }

        public static List<UserDTO> UserToUserDTO(List<User> users) {
            List<UserDTO> userDTOs = new List<UserDTO>();

            foreach (User user in users) { 
                userDTOs.Add(new UserDTO(user));
            }

            return userDTOs;
        }
    }
}