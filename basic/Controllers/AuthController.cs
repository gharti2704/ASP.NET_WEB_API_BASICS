using basic.Data;
using basic.Dtos;
using basic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace basic.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
  private readonly ApplicationDbContext _context;
  public AuthController(ApplicationDbContext context)
  {
    _context = context;
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

    var passwordHash = BCrypt.Net.BCrypt.HashPassword(userForRegistrationDto.Password);

    var authToAdd = new Auth
    {
      Email = userForRegistrationDto.Email,
      PasswordHash = passwordHash,
    };

    Console.WriteLine(authToAdd.ToString());

    try
    {
      await _context.SaveChangesAsync();
      return Ok();
    }
    catch (Exception e)
    {

      throw new Exception(e.Message);
    }
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
  {
    try
    {
      var userForConfirmation = await _context.Auths.FirstOrDefaultAsync(u => u.Email == userForLoginDto.Email);
      if (userForConfirmation == null)
      {
        return Unauthorized();
      }
      
      var isCorrectPassword = BCrypt.Net.BCrypt.Verify(userForLoginDto.Password, userForConfirmation.PasswordHash);
      return isCorrectPassword ? Ok() : StatusCode(401, "Incorrect password");
    }
    catch (Exception e)
    {
      throw new Exception(e.Message);
    }
  }
}