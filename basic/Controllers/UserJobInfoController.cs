using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using basic.Models;
using basic.Data;
using basic.Dtos;

namespace basic.Controllers;

[ApiController]
[Route("api")]

public class UserJobInfoController : ControllerBase
{
  private readonly IMapper _mapper;
  private readonly IUserJobInfoReposiotry _userJobInfoReposiotry;
  private readonly ICommonRepository _commonRepository;
  public UserJobInfoController(IMapper mapper, IUserJobInfoReposiotry userJobInfoReposiotry, ICommonRepository commonRepository)
  {
    _mapper = mapper;
    _userJobInfoReposiotry = userJobInfoReposiotry;
    _commonRepository = commonRepository;
  }

  [HttpGet("usersjobinfo")]
  public async Task<IActionResult> GetUserJobInfos()
  {
    try
    {
      var userJobInfoItems = await _userJobInfoReposiotry.GetUserJobInfos();
      return Ok(userJobInfoItems);
    }
    catch (Exception ex)
    {
      return BadRequest($"Couldn't retrieve user job info: {ex.Message}");
    }
  }

  [HttpGet("usersjobinfo/{userId}")]
  public async Task<UserJobInfo> GetUserJobInfo(int userId)
  {
    try
    {
      var userJobInfoItem = await _userJobInfoReposiotry.GetUserJobInfo(userId);
      return userJobInfoItem;
    }
    catch (Exception ex)
    {
      throw new Exception($"Couldn't retrieve user job info: {ex.Message}");
    }
  }

  [HttpPut("usersjobinfo/{userId}")]
  public async Task<IActionResult> UpdateUserJobInfo(int userId, UserJobInfo userJobInfo)
  {
    try
    {
      var userJobInfoFromRepo = await _userJobInfoReposiotry.GetUserJobInfo(userId);
      var userJobInfoUpdated = await _userJobInfoReposiotry.UpdateUserJobInfo(userId, userJobInfo);
      return CreatedAtAction(nameof(GetUserJobInfo), new { userId = userJobInfoUpdated.UserId }, userJobInfoUpdated);
    }
    catch (Exception ex)
    {
      return BadRequest($"Couldn't update user job info: {ex.Message}");
    }
  }

  [HttpPost("usersjobinfo")]
  public async Task<IActionResult> CreateUserJobInfo(UserJobInfoToAddDto userJobInfo)
  {
    try
    {
      if (userJobInfo == null) throw new Exception("User job info data is empty");
      var userJobInfoToAdd = _mapper.Map<UserJobInfo>(userJobInfo);
      _commonRepository.AddEntity<UserJobInfo>(userJobInfoToAdd);
      await _commonRepository.SaveChangesAsync();
      return CreatedAtAction(nameof(CreateUserJobInfo), new { userId = userJobInfoToAdd.UserId }, userJobInfoToAdd);
    }
    catch (Exception ex)
    {
      return BadRequest($"Couldn't create user job info: {ex.Message}");
    }
  }

  [HttpDelete("usersjobinfo/{userId}")]
  public async Task<IActionResult> DeleteUserJobInfo(int userId)
  {
    try
    {
      var userJobInfoFromRepo = await _userJobInfoReposiotry.GetUserJobInfo(userId);
      _commonRepository.DeleteEntity<UserJobInfo>(userJobInfoFromRepo);
      await _commonRepository.SaveChangesAsync();
      return Ok();
    }
    catch (Exception ex)
    {
      return BadRequest($"Couldn't delete user job info: {ex.Message}");
    }
  }

}