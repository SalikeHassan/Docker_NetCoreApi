using Microsoft.EntityFrameworkCore;
using TestProject.ZipPay.Domain.Entities;

namespace TestProject.ZipPay.Infrastructure.Context
{
    /// <summary>
    /// DbContext class for zip pay test project
    /// </summary>
    public class ZipPayContext : DbContext
    {
        /// <summary>
        /// DbSet property
        /// Property use for query and save, update instance of user
        /// </summary>
        public virtual DbSet<User> User { get; set; }

        /// <summary>
        /// DbSet property
        /// Property use for query and save, update instance of account
        /// </summary>
        public virtual DbSet<Account> Account { get; set; }

        public ZipPayContext(DbContextOptions options) : base(options)
        {
            
        }

        /// <summary>
        /// Overriden method to load the entity configurations
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
