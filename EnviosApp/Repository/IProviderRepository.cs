﻿using EnviosApp.Models;

namespace EnviosApp.Repository.Implementation {
    public interface IProviderRepository {
        IEnumerable<Provider> GetProvidersWithZones();
        Provider GetProvider(string name);
        Provider GetProviderById(long id);

        void Save(Provider provider);
        void Update(Provider provider);
        void Remove(Provider provider);
        Provider GetProviderNoCountry(string name);
        Provider GetProviderByIdNoCountry(long id);
        Provider GetProviderOnly(string name);
    }
}