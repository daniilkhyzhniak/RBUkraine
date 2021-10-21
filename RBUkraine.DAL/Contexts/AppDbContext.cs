using Microsoft.EntityFrameworkCore;
using RBUkraine.DAL.Entities;
using RBUkraine.DAL.Extensions;

namespace RBUkraine.DAL.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(g => g.Email)
                .IsUnique();

            modelBuilder.Entity<Role>()
                .HasIndex(g => g.Name)
                .IsUnique();

            modelBuilder.AddSeedData();
            // modelBuilder.AddSoftDeletedFilter();
        }

        public DbSet<User> Users { get; set; }
        
        public DbSet<Role> Roles { get; set; }
        
        public DbSet<UserRole> UserRoles { get; set; }
        
        public DbSet<Animal> Animals { get; set; }

        public DbSet<AnimalImage> AnimalImages { get; set; }

        public DbSet<AnimalTranslate> AnimalTranslates { get; set; }
        
        public DbSet<CharitableOrganization> CharitableOrganizations { get; set; }

        public DbSet<CharitableOrganizationImage> CharitableOrganizationImages { get; set; }

        public DbSet<CharitableOrganizationTranslate> CharitableOrganizationTranslates { get; set; }

        public DbSet<CharityEvent> CharityEvents { get; set; }

        public DbSet<CharityEventTranslate> CharityEventTranslates { get; set; }
    }
}