using basic.Data;
using basic.Data.Repositories.Common;
using basic.Data.Repositories.UserRepository;
using basic.Data.Repositories.JobInfo;
using basic.Data.Repositories.Salary;
using Microsoft.EntityFrameworkCore;

// Load environment variables from .env file
DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
// Add DBContext for sql server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? throw new Exception("DB_CONNECTION_STRING not found in .env file"))
);

// Inject Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICommonRepository, CommonRepository>();
builder.Services.AddScoped<IUserJobInfoRepository, UserJobInfoRepository>();
builder.Services.AddScoped<IUserSalaryRepository, UserSalaryRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Configure CORS
builder.Services.AddCors(options =>
{
  options.AddPolicy("DevCors", build =>
  {
    build.WithOrigins("http://localhost:3000", "https://localhost:5000")
          .AllowAnyMethod()
          .AllowAnyHeader()
          .AllowCredentials();
  });
  options.AddPolicy("ProdCors", build =>
  {
    build.WithOrigins("https://mydomain.com")
          .AllowAnyMethod()
          .AllowAnyHeader()
          .AllowCredentials();
  });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseCors("DevCors");
  app.UseSwagger();
  app.UseSwaggerUI();
}
else
{
  app.UseCors("ProdCors");
  app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
