using e_commerce.Server.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): 
            base(options)
        { 
        }

        public DbSet<e_commerce.Server.Models.Product> Products { get; set; }
        public DbSet<e_commerce.Server.Models.Category> Categories { get; set; }
    }
}
