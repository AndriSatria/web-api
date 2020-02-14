using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
    public class WebApiDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<PurchaseTransaction> PurchaseTransactions { get; set; }
        public virtual DbSet<PurchaseTransactionDetail> PurchaseTransactionDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PurchaseTransaction>()
                .HasMany(d => d.PurchaseTransactionDetails)
                .WithOne(h => h.PurchaseTransaction)
                .IsRequired();
        }

        public WebApiDbContext(DbContextOptions<WebApiDbContext> options) : base(options)
        {
        }
    }
}
