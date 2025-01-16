using EnviosApp.Models;

namespace EnviosApp.Repository.Implementation {
    public class ReceiverRepository : RepositoryBase<Receiver>, IReceiverRepository {
        public ReceiverRepository(EnviosDBContext enviosDBContext) : base(enviosDBContext) {

        }

        public void UpdateReceiver(Receiver receiver) {
            Update(receiver);
            SaveChanges();

        }

        public void RemoveReceiver(Receiver receiver) {
            Delete(receiver);
            SaveChanges();
        }

        public Receiver FindById(long id) {
            return FindByCondition(x => x.Id == id).FirstOrDefault();
        }

        public Receiver FindByReceiverName(string name) {
            return FindByCondition(x => x.Name == name).FirstOrDefault();
        }

        public IEnumerable<Receiver> GetAllReceivers() {
            return FindAll().ToList();
        }

        public void Save(Receiver Receiver) {
            Create(Receiver);
            SaveChanges();
        }
    }
}
