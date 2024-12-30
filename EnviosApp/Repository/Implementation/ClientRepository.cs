using EnviosApp.Models;

namespace EnviosApp.Repository.Implementation {
    public class ClientRepository : RepositoryBase<Client>, IClientRepository {
        public ClientRepository(EnviosDBContext repositoryContext) : base(repositoryContext) {
        }

        public Client FindById(long id) {
            return FindByCondition(cl => cl.Id == id).FirstOrDefault();
        }

        public IEnumerable<Client> GetAllClients() {
            return FindAll().ToList();
        }

        public void RemoveUser(Client client) {
            throw new NotImplementedException();
        }

        public void Save(Client client) {
            throw new NotImplementedException();
        }

        public void UpdateUser(Client client) {
            throw new NotImplementedException();
        }
    }
}
