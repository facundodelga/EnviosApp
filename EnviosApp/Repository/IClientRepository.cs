using EnviosApp.Models;

namespace EnviosApp.Repository {
    public interface IClientRepository {
        IEnumerable<Client> GetAllClients();
        void Save(Client client);
        Client FindById(long id);
        IEnumerable<Client> FindByOrganization(string organization);
        void RemoveUser(Client client);
        void UpdateClient(Client client);
        Client FindByEmail(string email);
    }
}
