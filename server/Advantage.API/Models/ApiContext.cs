using Microsoft.EntityFrameworkCore;

namespace Advantage.API
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Server> Servers { get; set; }
    }
}