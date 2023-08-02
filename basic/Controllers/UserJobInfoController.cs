using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using basic.Models;
using basic.Data.Repositories.Common;
using basic.Data.Repositories.JobInfo;
using basic.Dtos;

namespace basic.Controllers;

[ApiController]
[Route("api")]

public class UserJobInfoController : ControllerBase
{
  private readonly IMapper _mapper;
  private readonly IUserJobInfoRepository _jobInfo;
  private readonly ICommonRepository _commonRepository;
  public UserJobInfoController(IMapper mapper, IUserJobInfoRepository jobInfo, ICommonRepository commonRepository)
  {
    _mapper = mapper;
    _jobInfo = jobInfo;
    _commonRepository = commonRepository;
  }

  [HttpGet("jobsinfo")]
  public async Task<IActionResult> GetUserJobInfos()
  {
    try
    {
      var jobInfoItems = await _jobInfo.GetUserJobInfos();
      return Ok(jobInfoItems);
    }
    catch (Exception ex)
    {
      return BadRequest($"Couldn't retrieve user job info: {ex.Message}");
    }
  }

  [HttpGet("jobsinfo/{userId}")]
  public async Task<UserJobInfo> GetUserJobInfo(int userId)
  {
    try
    {
      var jobInfoItem = await _jobInfo.GetUserJobInfo(userId);
      return jobInfoItem;
    }
    catch (Exception ex)
    {
      throw new Exception($"Couldn't retrieve user job info: {ex.Message}");
    }
  }

  [HttpPut("jobsinfo/{userId}")]
  public async Task<IActionResult> UpdateUserJobInfo(int userId, UserJobInfo userJobInfo)
  {
    try
    {
      // var userJobInfoFromRepo = await _jobInfo.GetUserJobInfo(userId);
      var updatedJobInfo = await _jobInfo.UpdateUserJobInfo(userId, userJobInfo);
      return CreatedAtAction(nameof(GetUserJobInfo), new { userId = updatedJobInfo.UserId }, updatedJobInfo);
    }
    catch (Exception ex)
    {
      return BadRequest($"Couldn't update user job info: {ex.Message}");
    }
  }

  [HttpPost("jobsinfo")]
  public async Task<IActionResult> CreateUserJobInfo(UserJobInfoToAddDto userJobInfo)
  {
    try
    {
      if (userJobInfo == null) throw new Exception("User job info data is empty");
      var jobInfoToAdd = _mapper.Map<UserJobInfo>(userJobInfo);
      _commonRepository.AddEntity<UserJobInfo>(jobInfoToAdd);
      await _commonRepository.SaveChangesAsync();
      return CreatedAtAction(nameof(CreateUserJobInfo), new { userId = jobInfoToAdd.UserId }, jobInfoToAdd);
    }
    catch (Exception ex)
    {
      return BadRequest($"Couldn't create user job info: {ex.Message}");
    }
  }

  [HttpDelete("jobsinfo/{userId}")]
  public async Task<IActionResult> DeleteUserJobInfo(int userId)
  {
    try
    {
      var jobInfoFromRepo = await _jobInfo.GetUserJobInfo(userId);
      _commonRepository.DeleteEntity<UserJobInfo>(jobInfoFromRepo);
      await _commonRepository.SaveChangesAsync();
      return Ok();
    }
    catch (Exception ex)
    {
      return BadRequest($"Couldn't delete user job info: {ex.Message}");
    }
  }

}