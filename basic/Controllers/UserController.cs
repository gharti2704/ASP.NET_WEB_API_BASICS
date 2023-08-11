using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using basic.Models;
using basic.Data.Repositories.Common;
using basic.Data.Repositories.UserRepository;
using basic.Dtos;

namespace basic.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
  private readonly IMapper _mapper;
  private readonly IUserRepository _userRepository;
  private readonly ICommonRepository _commonRepository;
  public UserController(IMapper mapper, IUserRepository userRepository, ICommonRepository commonRepository)
  {
    _mapper = mapper;
    _userRepository = userRepository;
    _commonRepository = commonRepository;
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
  [HttpGet("{userId:int:min(1)}")]
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
  [HttpPost]
  public async Task<IActionResult> CreateUser(UserToAddDto user)
  {
    try
    {
      if (user == null) throw new Exception("User data is empty");
      var userToAdd = _mapper.Map<User>(user);
      _commonRepository.AddEntity(userToAdd);
      await _commonRepository.SaveChangesAsync();
      return CreatedAtAction(nameof(GetUser), new { userId = userToAdd.UserId }, userToAdd);
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }

  [HttpPut("{userId:int:min(1)}")]
  public async Task<IActionResult> UpdateUser(int userId, User user)
  {
    try
    {
      var updatedUser = await _userRepository.UpdateUser(userId, user);
      return await _commonRepository.SaveChangesAsync() ? Ok(updatedUser) : throw new Exception("Error updating user");
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }

  [HttpDelete("{userId:int:min(1)}")]
  public async Task<IActionResult> DeleteUser(int userId)
  {
    try
    {
      User userToDelete = await _userRepository.GetUser(userId);
      _commonRepository.DeleteEntity<User>(userToDelete);
      return await _commonRepository.SaveChangesAsync() ? Ok(userToDelete) : throw new Exception("Error deleting user");
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }
}
