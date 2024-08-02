using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NoteSaverAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>().ToTable("posts");
        }
    }
}