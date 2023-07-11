using Microsoft.EntityFrameworkCore;
using basic.Models;

namespace basic.Data;
public class ApplicationDbContext : DbContext
{
  // private readonly IConfiguration _configuration;
  public DbSet<User> Users { get; set; } = null!;
  public DbSet<UserJobInfo> UserJobInfo { get; set; } = null!;
  public DbSet<UserSalary> UserSalary { get; set; } = null!;
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
    // modelBuilder.HasDefaultSchema("BasicWebAPI");
    modelBuilder.Entity<User>()
    .ToTable("Users", "BasicWebAPI")
    .HasKey(user => user.UserId);

    modelBuilder.Entity<UserJobInfo>()
    .ToTable("UserJobInfo", "BasicWebAPI")
    .HasKey(user => user.UserId);

    modelBuilder.Entity<UserSalary>()
    .ToTable("UserSalary", "BasicWebAPI")
    .HasKey(user => user.UserId);
  }

}