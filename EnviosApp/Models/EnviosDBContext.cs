using Microsoft.EntityFrameworkCore;

namespace EnviosApp.Models {
    public class EnviosDBContext : DbContext {
        public EnviosDBContext(DbContextOptions<EnviosDBContext> dbContextOptions) : base(dbContextOptions) {        }

        public DbSet<Client> User {get; set;}
        public DbSet<Client> Client { get; set; }

    }
}
