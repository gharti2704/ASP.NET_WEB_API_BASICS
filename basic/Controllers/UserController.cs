using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using basic.Models;
using basic.Data;
using basic.Dtos;

namespace basic.Controllers;

[ApiController]
[Route("api")]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    public UserController(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            var users = await _userRepository.GetUsers();
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
            var user = await _userRepository.GetUser(userId);
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
            _userRepository.AddEntity<User>(userToAdd);
            await _userRepository.SaveChangesAsync();
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
            var updatedUser = await _userRepository.UpdateEntity<User>(userId, user);
            return await _userRepository.SaveChangesAsync() ? Ok(updatedUser) : throw new Exception("Error updating user");

            // var userFromDb = await _userRepository.GetUser(userId) ?? throw new Exception("User not found");
            // user.UserId = userFromDb.UserId;
            // var userToUpdate = _mapper.Map<User>(user);
            // _userRepository.UpdateEntity<User>(userToUpdate);
            // return await _userRepository.SaveChangesAsync() ? CreatedAtAction(nameof(GetUser), new { userId = userToUpdate.UserId }, userToUpdate) : throw new Exception("Error updating user");
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
            User userToDelete = await _userRepository.GetUser(userId);
            _userRepository.DeleteEntity<User>(userToDelete);
            return await _userRepository.SaveChangesAsync() ? Ok(userToDelete) : throw new Exception("Error deleting user");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
