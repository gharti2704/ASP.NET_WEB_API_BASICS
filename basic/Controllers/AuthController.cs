using basic.Data;
using basic.Dtos;
using basic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using basic.Data.Repositories.Common;
using basic.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace basic.Controllers;
[Authorize]
public class AuthController : ControllerBase
{
  private readonly ApplicationDbContext _context;
  private readonly IMapper _mapper;
  private readonly ICommonRepository _commonRepository;
  private readonly AuthHelper _authHelper;

  public AuthController(ApplicationDbContext context, IMapper mapper, ICommonRepository commonRepository)
  {
    _context = context;
    _mapper = mapper;
    _commonRepository = commonRepository;
    _authHelper = new AuthHelper();
  }

  [AllowAnonymous]
  [HttpPost("register")]
  public async Task<ActionResult<UserForRegistrationDto>> Register(UserForRegistrationDto userForRegistrationDto)
  {
    if (userForRegistrationDto == null) throw new Exception("User data is empty");

    var user = new User
    {
      Email = userForRegistrationDto.Email,
      FirstName = userForRegistrationDto.FirstName,
      LastName = userForRegistrationDto.LastName,
      Gender = userForRegistrationDto.Gender
    };

    var userToAdd = _mapper.Map<User>(user);
    _commonRepository.AddEntity(userToAdd);
    await _commonRepository.SaveChangesAsync();

    if (userForRegistrationDto.Password != userForRegistrationDto.ConfirmPassword)
    {
      return BadRequest("Password and Confirm Password must match");
    }

    if (await _context.Auths.AnyAsync(u => u.Email == userForRegistrationDto.Email))
    {
      return BadRequest("Email already exists");
    }

    var passwordHash = BCrypt.Net.BCrypt.HashPassword(userForRegistrationDto.Password);

    var authToAdd = new Auth
    {
      Email = userForRegistrationDto.Email,
      PasswordHash = passwordHash,
    };

    try
    {
      await _context.Auths.AddAsync(authToAdd);
      await _context.SaveChangesAsync();
      return Ok();
    }
    catch (Exception e)
    {
      throw new Exception(e.Message);
    }
  }

  [AllowAnonymous]
  [HttpPost("login")]
  public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
  {
    try
    {
      var userForConfirmation = await _context.Auths.FirstOrDefaultAsync(u => u.Email == userForLoginDto.Email);
      var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userForLoginDto.Email);
      if (userForConfirmation is null || user is null)
      {
        return Unauthorized();
      }

      var isCorrectPassword = BCrypt.Net.BCrypt.Verify(userForLoginDto.Password, userForConfirmation.PasswordHash);
        
      return isCorrectPassword ? Ok(new Dictionary<string, string>{
        {"token", _authHelper.CreateToken(user.UserId)}
      }) : StatusCode(403, "Incorrect password");
    }
    catch (Exception e)
    {
      throw new Exception(e.Message);
    }
  }

  [HttpGet("refreshToken")]
  public async Task<ActionResult> RefreshToken()
  {
    var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId.ToString() == User.FindFirst("userId")!.Value);
    if (user is null)
    {
      return Unauthorized();
    }

    return Ok(new Dictionary<string, string>{
      {"token", _authHelper.CreateToken(user.UserId)}
    });
  }

}