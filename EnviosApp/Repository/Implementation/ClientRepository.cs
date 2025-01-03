﻿using EnviosApp.Models;

namespace EnviosApp.Repository.Implementation {
    public class ClientRepository : RepositoryBase<Client>, IClientRepository {
        public ClientRepository(EnviosDBContext repositoryContext) : base(repositoryContext) {
        }

        public Client FindById(long id) {
            return FindByCondition(cl => cl.Id == id).FirstOrDefault();
        }

        public Client FindByEmail(string email) {
            return FindByCondition(cl => cl.Email.Equals(email)).FirstOrDefault();
        }


        public IEnumerable<Client> FindByOrganization(string organization) {
            return FindByCondition(cl => cl.Organization.Equals(organization)).ToList();
        }

        public IEnumerable<Client> GetAllClients() {
            return FindAll().ToList();
        }

        public void RemoveClient(Client client) {
            Delete(client);
            SaveChanges();
        }

        public void Save(Client client) {
            Create(client);
            SaveChanges();
        }

        public void UpdateClient(Client client) {
            Update(client);
            SaveChanges();
        }
    }
}
