﻿using EnviosApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EnviosApp.Repository.Implementation {
    public class ProviderRepository : RepositoryBase<Provider>, IProviderRepository {
        public ProviderRepository(EnviosDBContext context) : base(context) { }

        public Provider GetProvider(string name) {
            return FindByCondition(p => p.Name.ToLower().Equals(name))
                .Include(p => p.ServiceTypes)
                .Include(p => p.Zones)
                .FirstOrDefault();
        }

        public Provider GetProviderById(long id) {
            return FindByCondition(p => p.Id == id)
                .Include(p => p.ServiceTypes)
                .Include(p => p.Zones)
                .FirstOrDefault();
        }

        public IEnumerable<Provider> GetProvidersWithZones() {
            return FindAll()
                .Include(p => p.ServiceTypes)
                .Include(p => p.Zones)
                .ToList();
        }

        public void Save(Provider provider) {
            Create(provider);
            SaveChanges();
        }
    }
}
