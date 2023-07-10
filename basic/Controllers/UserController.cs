using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using basic.Models;
using basic.Data;
using basic.Dtos;

namespace basic.Controllers;

[ApiController]
[Route("api")]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    // public UserController(IConfiguration configuration, IMapper mapper)
    // {
    //     _context = new ApplicationDbContext(configuration);
    //     _mapper = mapper;
    // }
    public UserController(IMapper mapper, ApplicationDbContext context)
    {
        _context = context;
        _mapper = mapper;
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
            return BadRequest(ex.Message);
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
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("user")]
    public async Task<IActionResult> CreateUser(UserToAddDto user)
    {
        try
        {
            if (user == null) throw new Exception("User data is empty");
            var userToAdd = _mapper.Map<User>(user);

            await _context.Users.AddAsync(userToAdd);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { userId = userToAdd.UserId }, userToAdd);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex.Message);
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
            return await _context.SaveChangesAsync() > 0 ? CreatedAtAction(nameof(GetUser), new { userId = userToUpdate.UserId }, userToUpdate) : throw new Exception("Error updating user");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("user/{userId}")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        try
        {
            User userToDelete = await _context.Users.FindAsync(userId) ?? throw new Exception("User not found");
            _context.Users.Remove(userToDelete);
            return await _context.SaveChangesAsync() > 0 ? Ok(userToDelete) : throw new Exception("Error deleting user");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
