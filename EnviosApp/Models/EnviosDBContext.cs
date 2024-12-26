using Microsoft.EntityFrameworkCore;

namespace EnviosApp.Models {
    public class EnviosDBContext : DbContext {
        public EnviosDBContext(DbContextOptions<EnviosDBContext> dbContextOptions) : base(dbContextOptions) {        }

        public DbSet<User> User {get; set;}

    }
}
