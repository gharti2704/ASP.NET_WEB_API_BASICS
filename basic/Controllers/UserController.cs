using basic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using basic.Models;

namespace basic.Controllers;

[ApiController]
[Route("api")]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public UserController(IConfiguration configuration)
    {
        _context = new ApplicationDbContext(configuration);
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            var users = await _context.Users.ToListAsync() ?? throw new Exception("No users found");
            return Ok(users);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUser(int userId)
    {
        try
        {
            var user = await _context.Users.FindAsync(userId) ?? throw new Exception("User not found");
            return Ok(user);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpPost("user")]
    public async Task<IActionResult> CreateUser(User user)
    {
        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("user/{userId}")]
    public async Task<IActionResult> UpdateUser(int userId, User user)
    {
        try
        {
            User userToUpdate = await _context.Users.FindAsync(userId) ?? throw new Exception("User not found");
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Email = user.Email;
            userToUpdate.Gender = user.Gender;
            userToUpdate.Active = user.Active;
            await _context.SaveChangesAsync();
            return Ok(userToUpdate);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}