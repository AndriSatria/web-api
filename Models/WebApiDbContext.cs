using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
    public class WebApiDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public WebApiDbContext(DbContextOptions<WebApiDbContext> options) : base(options)
        {
        }
    }
}
