using Microsoft.EntityFrameworkCore;
using WageCalculatorBackend.Models;

namespace WageCalculatorBackend.DbData
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TimeRule> Calculations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeRule>(entity =>
            {
                entity.ToTable("TimeRules");
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasColumnType("tinytext");

                entity.Property(c => c.Description)
                    .HasColumnType("varchar(50)");

                entity.Property(c => c.RegularStartTime)
                    .HasColumnType("tinyint");

                entity.Property(c => c.NightTimeStartTime)
                    .HasColumnType("tinyint");

                entity.Property(c => c.MidnightStartTime)
                    .HasColumnType("tinyint");
            });
        }
    }
}
