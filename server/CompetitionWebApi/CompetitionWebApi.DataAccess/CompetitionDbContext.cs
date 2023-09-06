using CompetitionWebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompetitionWebApi.DataAccess;

public class CompetitionDbContext : DbContext
{
    public CompetitionDbContext(DbContextOptions<CompetitionDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    public DbSet<Performance> Performances { get; set; }
    public DbSet<Score> Scores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>()
            .HasData(
                new Role { Id = 1, Name = "Spectator" },
                new Role { Id = 2, Name = "Contestant" },
                new Role { Id = 3, Name = "Judge" }
            );
    }
}
