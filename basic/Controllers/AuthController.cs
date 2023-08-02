using System.Security.Cryptography;
using System.Text;
using basic.Data;
using basic.Dtos;
using basic.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using AutoMapper;
using basic.Data.Repositories.Common;

namespace basic.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
  private readonly ApplicationDbContext _context;
  private readonly IMapper _mapper;
  private readonly ICommonRepository _commonRepository;
  public AuthController(ApplicationDbContext context, IMapper mapper, ICommonRepository commonRepository)
  {
    _context = context;
    _mapper = mapper;
    _commonRepository = commonRepository;
  }

  [HttpPost("register")]
  public async Task<ActionResult<UserForRegistrationDto>> Register(UserForRegistrationDto userForRegistrationDto)
  {
    if (userForRegistrationDto.Password != userForRegistrationDto.ConfirmPassword)
    {
      return BadRequest("Password and Confirm Password must match");
    }

    if (await _context.Auths.AnyAsync(u => u.Email == userForRegistrationDto.Email))
    {
      return BadRequest("Email already exists");
    }

    byte[] passwordSalt = new byte[128 / 8];

    using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
    {
      rng.GetNonZeroBytes(passwordSalt);
    }

    var passwordHash = GetPasswordHash(userForRegistrationDto.Password, passwordSalt);

    var authToAdd = new Auth
    {
      Email = userForRegistrationDto.Email,
      PasswordHash = passwordHash,
      PasswordSalt = passwordSalt
    };

    Console.WriteLine(authToAdd.ToString());

    // var sqlAddAuth = await _context.Auths.AddAsync(authToAdd);
    // await _context.SaveChangesAsync();


    string sqlAddAuth = @"
    INSERT INTO BasicWebAPI.Auths ([Email],
    [PasswordHash],
    [PasswordSalt]) VALUES (' " + userForRegistrationDto.Email +
    "' , @PasswordHash, @PasswordSalt)";

    var sqlParameters = new List<SqlParameter>();

    var passwordSaltParameter = new SqlParameter("@PasswordSalt", SqlDbType.VarBinary)
    {
      Value = passwordSalt
    };

    var passwordHashParameter = new SqlParameter("@PasswordHash", SqlDbType.VarBinary)
    {
      Value = passwordHash
    };

    sqlParameters.Add(passwordHashParameter);
    sqlParameters.Add(passwordSaltParameter);

    if (await Helpers.Helpers.ExecuteSqlWithParameters(sqlAddAuth, sqlParameters))
    {
      try
      {
        var user = new User()
        {
          Email = userForRegistrationDto.Email,
          FirstName = userForRegistrationDto.FirstName,
          LastName = userForRegistrationDto.LastName,
          Gender = userForRegistrationDto.Gender,
        };

        var userToAdd = _mapper.Map<User>(user);
        _commonRepository.AddEntity<User>(userToAdd);
        await _commonRepository.SaveChangesAsync();
        return CreatedAtAction(nameof(Register), new { userId = userToAdd.UserId }, userToAdd);
      }
      catch (System.Exception)
      {

        throw new Exception("Failed to add user");

      }
    }

    throw new Exception("Failed to register user.");
    //Store Procedure
    // await _context.Database.ExecuteSqlRawAsync("EXEC dbo.spUsers_Insert @PasswordHash, @PasswordSalt", sqlParameters);
    // var userToCreate = new User
    // {
    //   Email = userForRegistrationDto.Email
    // };
    // var createdUser = await _context.Users.AddAsync(userToCreate);
    // await _context.SaveChangesAsync();

  }

  [HttpPost("login")]
  public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
  {
    var userForConfirmation = await _context.Auths.FirstOrDefaultAsync(u => u.Email == userForLoginDto.Email);
    if (userForConfirmation == null)
    {
      return Unauthorized("Email or password didn't match");
    }
    var passwordHash = GetPasswordHash(userForLoginDto.Password, userForConfirmation.PasswordSalt);
    for (int index = 0; index < passwordHash.Length; index++)
    {
      if (passwordHash[index] != userForConfirmation.PasswordHash[index])
      {
        return StatusCode(401, "Incorrect password");
      }
    }
    return Ok();
  }

  private byte[] GetPasswordHash(string password, byte[] passwordSalt)
  {
    string base64PasswordKey = $"{Environment.GetEnvironmentVariable("PASSWORD_KEY")}{Convert.ToBase64String(passwordSalt)}";

    byte[] passwordHash = KeyDerivation.Pbkdf2(
      password: password,
      salt: Encoding.ASCII.GetBytes(base64PasswordKey),
      prf: KeyDerivationPrf.HMACSHA256,
      iterationCount: 10000,
      numBytesRequested: 256 / 8
    );

    return passwordHash;
  }
}