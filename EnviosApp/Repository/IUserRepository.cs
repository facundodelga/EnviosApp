using EnviosApp.Models;

namespace EnviosApp.Repository
{
    public interface IUserRepository {
        IEnumerable<User> GetAllUsers();
        void Save(User user);
        User FindById(long id);
        User FindByUserName(string username);
        void RemoveUser(User user);
        void UpdateUser(User user);
    }
}
