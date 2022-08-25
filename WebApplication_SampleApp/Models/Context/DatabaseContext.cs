using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using WebApplication_SampleApp.Models.Entities;

namespace WebApplication_SampleApp.Models.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<EBulletin> EBulletins { get; set; }        
        public DbSet<OrderedService> OrderedServices { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=ServiceSystemDB; Trusted_Connection=true");
                optionsBuilder.UseLazyLoadingProxies();

            }
        }

    }
    



}
