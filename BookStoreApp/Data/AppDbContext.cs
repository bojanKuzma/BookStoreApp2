using BookStoreApp.Models;
using BookStoreApp.Services;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Author> Authors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source=BookStore.db");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        builder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();


        var (hash, salt) = PasswordService.HashPassword("admin123");
        builder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "admin",
                Email = "admin@bookstore.com",
                PasswordHash = "1yiBj3uI/hOHZhlIYgq573SOig6dRhpRw9Y+SbIVOKU=",
                PasswordSalt = "37074fe5d34548839f2b4a74984d4128",
                Role = "Admin"
            }
        );
    }
}