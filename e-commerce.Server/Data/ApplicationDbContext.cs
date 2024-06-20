using Microsoft.EntityFrameworkCore;

namespace e_commerce.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

        public DbSet<e_commerce.Server.Models.Product> Products { get; set; }
        public DbSet<e_commerce.Server.Models.Category> Categories { get; set; }
    }
}
