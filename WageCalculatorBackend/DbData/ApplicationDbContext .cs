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

        public DbSet<Calculation> Calculations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Calculation>(entity =>
            {
                entity.ToTable("Calculations");
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasColumnType("tinytext");

                entity.Property(c => c.Description)
                    .HasColumnType("varchar(50)");

                entity.Property(c => c.RegularStartTime)
                    .HasColumnType("time");

                entity.Property(c => c.NightTimeStartTime)
                    .HasColumnType("time");

                entity.Property(c => c.MidnightStartTime)
                    .HasColumnType("time");
            });
        }
    }
}
