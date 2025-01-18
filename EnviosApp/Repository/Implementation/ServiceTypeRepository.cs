using EnviosApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EnviosApp.Repository.Implementation
{
    public class ServiceTypeRepository : RepositoryBase<ServiceType>, IServiceTypeRepository {
        public ServiceTypeRepository(EnviosDBContext context) : base(context) { }

        public void Save(ServiceType newservice) {
            Create(newservice);
            SaveChanges();  
        }
    }
}
