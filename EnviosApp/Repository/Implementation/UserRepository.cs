using EnviosApp.Models;

namespace EnviosApp.Repository.Implementation {
    public class UserRepository : RepositoryBase<Client>, IUserRepository {
        public UserRepository(EnviosDBContext enviosDBContext) : base(enviosDBContext) {
            
        }

        public void UpdateUser(Client user) {
            Update(user);
            SaveChanges();
        
        }

        public void RemoveUser(Client user) {
            Delete(user);
            SaveChanges();
        }

        public Client FindById(long id) {
            return FindByCondition(x => x.Id == id).FirstOrDefault();
        }

        public Client FindByUserName(string username) {
            return FindByCondition(x => x.UserName == username).FirstOrDefault();
        }

        public IEnumerable<Client> GetAllUsers() {
            return FindAll().ToList();
        }

        public void Save(Client user) {
            Create(user);
            SaveChanges();
        }


    }
}
