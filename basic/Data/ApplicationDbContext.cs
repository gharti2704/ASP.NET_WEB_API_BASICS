using Microsoft.EntityFrameworkCore;
using basic.Models;

namespace basic.Data;
public class ApplicationDbContext : DbContext
{
    // private readonly IConfiguration _configuration;
    public DbSet<User> Users { get; set; }
    public DbSet<User> UsersJobInfo { get; set; }
    public DbSet<User> UsersSalary { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    // public ApplicationDbContext(IConfiguration configuration)
    // {
    //     _configuration = configuration;
    // }
    // protected override void OnConfiguring(DbContextOptionsBuilder options)
    // {
    //     if (!options.IsConfigured)
    //     {
    //         var DbUrl = Environment.GetEnvironmentVariable("DB_URL");
    //         options.UseSqlServer(DbUrl,
    //         options => options.EnableRetryOnFailure());
    //     }
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("BasicWebAPI");
        modelBuilder.Entity<User>()
        .ToTable("Users", "BasicWebAPI")
        .HasKey(user => user.UserId);

        modelBuilder.Entity<UserJobInfo>()
        .ToTable("UsersJobInfo", "BasicWebAPI")
        .HasKey(user => user.UserId);

        modelBuilder.Entity<UserSalary>()
        .ToTable("UsersSalary", "BasicWebAPI")
        .HasKey(user => user.UserId);
    }

}