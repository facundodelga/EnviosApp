using EnviosApp.Models;

namespace EnviosApp.Repository.Implementation {
    public class UserRepository : RepositoryBase<User>, IUserRepository {
        public UserRepository(EnviosDBContext enviosDBContext) : base(enviosDBContext) {
            
        }

        public void UpdateUser(User user) {
            Update(user);
            SaveChanges();
        
        }

        public void RemoveUser(User user) {
            Delete(user);
            SaveChanges();
        }

        public User FindById(long id) {
            return FindByCondition(x => x.Id == id).FirstOrDefault();
        }

        public User FindByUserName(string username) {
            return FindByCondition(x => x.UserName.Equals(username)).FirstOrDefault();
        } 

        public IEnumerable<User> GetAllUsers() {
            return FindAll().ToList();
        }

        public void Save(User user) {
            Create(user);
            SaveChanges();
        }


    }
}
