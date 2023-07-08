using Microsoft.EntityFrameworkCore;
using basic.Models;

namespace basic.Data;
public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DbSet<User> Users { get; set; }
    public DbSet<User> UsersJobInfo { get; set; }
    public DbSet<User> UsersSalary { get; set; }
    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"),
            options => options.EnableRetryOnFailure());
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("BasicWebAPI");
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<UserJobInfo>().ToTable("UsersJobInfo");
        modelBuilder.Entity<UserSalary>().ToTable("UsersSalary");
    }
}