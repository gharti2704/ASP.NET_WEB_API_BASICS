using System.Text;
using basic.Data;
using basic.Data.Repositories.Common;
using basic.Data.Repositories.UserRepository;
using basic.Data.Repositories.JobInfo;
using basic.Data.Repositories.Posts;
using basic.Data.Repositories.Salary;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
builder.Services.AddScoped<ICompleteUserRepository, CompleteUserRepository>();
builder.Services.AddScoped<ICommonRepository, CommonRepository>();
builder.Services.AddScoped<IUserJobInfoRepository, UserJobInfoRepository>();
builder.Services.AddScoped<IUserSalaryRepository, UserSalaryRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICompletePostRepository, CompletePostRepository>();
builder.Services.AddControllers();

//Configure jwt authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
    options.TokenValidationParameters = new TokenValidationParameters()
    {
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_TOKEN_KEY")!)),
      ValidateIssuer = false,
      ValidateAudience = false,
    };
  });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Configure CORS
builder.Services.AddCors(options =>
{
  options.AddPolicy("DevCors", build =>
  {
    build.WithOrigins("http://localhost:3000", "https://localhost:8000")
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

//Make sure UseAuthentication comes before UseAuthorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
