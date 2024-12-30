namespace EnviosApp.Models.DTOs {
    public class UserDTO {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }

        public UserDTO(User user) {
            Id = user.Id;
            Name = user.Name;
            Username = user.UserName;
            Role = user.Role;
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
