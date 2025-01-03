using EnviosApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EnviosApp.Repository.Implementation
{
    public class ServiceTypeRepository : RepositoryBase<ServiceType>, IServiceTypeRepository {
        public ServiceTypeRepository(DbContext context) : base(context) { }


    }
}
