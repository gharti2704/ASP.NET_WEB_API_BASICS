using Microsoft.AspNetCore.Mvc;
using basic.Models;
using basic.Data.Repositories.UserRepository;

namespace basic.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserCompleteController : ControllerBase
{
  private readonly ICompleteUserRepository _completeUserRepository;
  public UserCompleteController(ICompleteUserRepository userCompleteRepository)
  {
    _completeUserRepository = userCompleteRepository;
  }

  //Retrieve all users
  [HttpGet("users")]
  public async Task<IActionResult> GetUsers()
  {
    try
    {
      var users = await _completeUserRepository.GetCompleteUsers();
      return Ok(users);
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }
  
  //Retrieve a single user
  [HttpGet("{userId:int:min(1)}")]
  public async Task<IActionResult> GetUser(int userId)
  {
    try
    {
      var user = await _completeUserRepository.GetCompleteUser(userId);
      return Ok(user);
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }

  //Update a user
  [HttpPut("update")]
  public IActionResult UpdateUser(UserComplete user)
  {
    try
    {
      var update = _completeUserRepository.UpdateCompleteUser(user);
      return Ok(update);
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }

  
  //Delete a user
  [HttpDelete("{userId:int:min(1)}")]
  public IActionResult DeleteUser(int userId)
  {
    try
    {
      var delete = _completeUserRepository.DeleteCompleteUser(userId);
      return Ok(delete);
    }
    catch (Exception ex)
    {
      return BadRequest(ex.Message);
    }
  }
}
