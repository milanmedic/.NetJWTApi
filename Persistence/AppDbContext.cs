using Microsoft.EntityFrameworkCore;
using JwtApi.Domain.Models;
using System.Linq;

namespace JwtApi.Persistence {
    public class AppDbContext : DbContext{
        public DbSet<User> Users {get; set;}
        public DbSet<Project> Projects {get; set;}
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(p => p.Id);
            builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<User>().Property(p => p.Email).IsRequired().HasMaxLength(50);
            builder.Entity<User>().Property(p => p.PasswordHash).IsRequired();
            builder.Entity<User>().Property(p => p.PasswordSalt).IsRequired();
            builder.Entity<User>().Property(p => p.Role).IsRequired();
            builder.Entity<User>().HasMany(p => p.Projects).WithOne(p => p.Creator).HasForeignKey(p => p.CreatorId);


            builder.Entity<Project>().ToTable("Projects");
            builder.Entity<Project>().HasKey(p => p.Id);
            builder.Entity<Project>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Project>().Property(p => p.Name).IsRequired().HasMaxLength(150);
            builder.Entity<Project>().Property(p => p.Description).IsRequired().HasMaxLength(400);
        }
    }
}