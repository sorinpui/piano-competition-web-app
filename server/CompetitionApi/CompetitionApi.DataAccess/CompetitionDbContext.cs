using CompetitionApi.Domain.Entities;
using CompetitionApi.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CompetitionApi.DataAccess
{
    public class CompetitionDbContext : DbContext
    {
        public CompetitionDbContext(DbContextOptions<CompetitionDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Rendition> Renditions { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var userBuilder = modelBuilder.Entity<User>();
            var roleBuilder = modelBuilder.Entity<Role>();

            userBuilder
                .ToTable("Users")
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<UserToRole>();

            userBuilder
                .HasIndex(u => u.Email)
                .IsUnique();

            roleBuilder
                .HasIndex(r => r.Name)
                .IsUnique();

            IEnumerable<Role> roles = new List<Role>()
            {
                new Role((int)UserRole.Spectator, UserRole.Spectator.ToString()),
                new Role((int)UserRole.Contestant, UserRole.Contestant.ToString()),
                new Role((int)UserRole.Admin, UserRole.Admin.ToString()),
                new Role((int)UserRole.Baroque_Judge, UserRole.Baroque_Judge.ToString()),
                new Role((int)UserRole.Classical_Judge, UserRole.Classical_Judge.ToString()),
                new Role((int)UserRole.Romantic_Judge, UserRole.Romantic_Judge.ToString())
            };

            roleBuilder
                .ToTable("Roles")
                .HasData(roles);
        }
    }
}
