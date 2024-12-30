using EnviosApp.Models;

namespace EnviosApp.Repository.Implementation {
    public class ClientRepository : RepositoryBase<Client>, IClientRepository {
        public ClientRepository(EnviosDBContext repositoryContext) : base(repositoryContext) {
        }

        public Client FindById(long id) {
            return FindByCondition(cl => cl.Id == id).FirstOrDefault();
        }

        public IEnumerable<Client> FindByOrganization(string organization) {
            return FindByCondition(cl => cl.Organization.Equals(organization)).ToList();
        }

        public IEnumerable<Client> GetAllClients() {
            return FindAll().ToList();
        }

        public void RemoveUser(Client client) {
            Delete(client);
            SaveChanges();
        }

        public void Save(Client client) {
            Create(client);
            SaveChanges();
        }

        public void UpdateUser(Client client) {
            Update(client);
            SaveChanges();
        }
    }
}
