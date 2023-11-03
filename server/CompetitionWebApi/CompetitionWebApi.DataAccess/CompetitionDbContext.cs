using CompetitionWebApi.Domain.Entities;
using CompetitionWebApi.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CompetitionWebApi.DataAccess;

public class CompetitionDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    public DbSet<Performance> Performances { get; set; }
    public DbSet<Score> Scores { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public CompetitionDbContext(DbContextOptions<CompetitionDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Role[] roles = new Role[]
        {
            new Role { Id = 1, Name = RoleType.Contestant.ToString() },
            new Role { Id = 2, Name = RoleType.Spectator.ToString() },
            new Role { Id = 3, Name = RoleType.Judge.ToString() },
            new Role { Id = 4, Name = RoleType.Admin.ToString() }
        };

        modelBuilder.Entity<Role>().HasData(roles);
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

        modelBuilder.Entity<User>()
            .HasMany(e => e.Roles)
            .WithMany(e => e.Users)
            .UsingEntity<UserRole>();
    }
}
