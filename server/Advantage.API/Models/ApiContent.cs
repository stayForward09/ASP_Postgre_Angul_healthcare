using Microsoft.EntityFrameworkCore;

namespace Advantage.API.Models
{
    public class ApiContent : DbContext
    {
        public ApiContext(DbContextOptions<ApiContent> options) : base(options) { }

        public DbSet<Customer> Customers { get; set;}
        public DbSet<Order> Orders { get; set;}
        public DbSet<Server> Servers { get; set;}

    }
}