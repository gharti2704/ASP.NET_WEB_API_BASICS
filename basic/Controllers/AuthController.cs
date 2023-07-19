using System.Security.Cryptography;
using System.Text;
using basic.Data;
using basic.Dtos;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace basic.Controllers;
public class AuthController : ControllerBase
{
  private readonly ApplicationDbContext _context;
  public AuthController(ApplicationDbContext context)
  {
    _context = context;
  }
  [HttpPost("register")]
  public async Task<IActionResult> Register(UserForRegistrationDto userForRegistrationDto)
  {
    if (userForRegistrationDto.Password != userForRegistrationDto.ConfirmPassword)
    {
      return BadRequest("Password and Confirm Password must match");
    }
    if (await _context.Users.AnyAsync(u => u.Email == userForRegistrationDto.Email))
    {
      return BadRequest("Email already exists");
    }
    byte[] passwordSalt = new byte[128 / 8];
    using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
    {
      rng.GetNonZeroBytes(passwordSalt);
    }

    string base64PasswordKey = $"{Environment.GetEnvironmentVariable("PASSWORD_KEY")}{Convert.ToBase64String(passwordSalt)}";
    byte[] passwordHash = KeyDerivation.Pbkdf2(
      password: userForRegistrationDto.Password,
      salt: Encoding.ASCII.GetBytes(base64PasswordKey),
      prf: KeyDerivationPrf.HMACSHA256,
      iterationCount: 10000,
      numBytesRequested: 256 / 8
    );

    List<SqlParameter> sqlParameters = new List<SqlParameter>()
    {
      new SqlParameter("@PasswordHash", passwordHash),
      new SqlParameter("@PasswordSalt", passwordSalt)
    };
      //Store Procedure
     await _context.Database.ExecuteSqlRawAsync("EXEC dbo.spUsers_Insert @PasswordHash, @PasswordSalt", sqlParameters);
    // var userToCreate = new User
    // {
    //   Email = userForRegistrationDto.Email
    // };
    // var createdUser = await _context.Users.AddAsync(userToCreate);
    // await _context.SaveChangesAsync();
    return StatusCode(201);
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
  {
    var userFromRepo = await _context.Users.FirstOrDefaultAsync(u => u.Email == userForLoginDto.Email);
    if (userFromRepo == null)
    {
      return Unauthorized();
    }
    return Ok();
  }
}