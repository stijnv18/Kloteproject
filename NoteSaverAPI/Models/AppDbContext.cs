using Microsoft.EntityFrameworkCore;

namespace NoteSaverAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Note> Posts { get; set; }
    }
}