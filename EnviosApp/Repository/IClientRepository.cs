using EnviosApp.Models;

namespace EnviosApp.Repository {
    public interface IClientRepository {
        IEnumerable<Client> GetAllUsers();
        void Save(Client user);
        Client FindById(long id);
        Client FindByUserName(string username);
        void RemoveUser(Client user);
        void UpdateUser(Client user);
    }
}
