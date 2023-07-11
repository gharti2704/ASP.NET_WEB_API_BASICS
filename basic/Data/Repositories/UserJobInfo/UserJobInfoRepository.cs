using basic.Models;
using Microsoft.EntityFrameworkCore;

namespace basic.Data;
public class UserJobInfoRepository : IUserJobInfoReposiotry
{
  private readonly ApplicationDbContext _context;
  public UserJobInfoRepository(ApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<UserJobInfo> GetUserJobInfo(int userJobInfoId)
  {
    try
    {
      return await _context.UserJobInfo.FindAsync(userJobInfoId) ?? throw new Exception($"Couldn't find user job info with id: {userJobInfoId}");
    }
    catch (Exception ex)
    {
      throw new Exception($"Couldn't find user job info: {ex.Message}");
    }
  }

  public async Task<IEnumerable<UserJobInfo>> GetUserJobInfos()
  {
    try
    {
      return await _context.UserJobInfo.ToListAsync();
    }
    catch (Exception ex)
    {
      throw new Exception($"Couldn't retrieve user job info: {ex.Message}");
    }
  }

  public async Task<UserJobInfo> UpdateUserJobInfo(int userJobInfoId, UserJobInfo userJobInfoToUpdate)
  {
    try
    {
      var userJobInfo = await _context.UserJobInfo.FindAsync(userJobInfoId) ?? throw new Exception($"Couldn't find user job info with id: {userJobInfoId}");
      userJobInfo.JobTitle = userJobInfoToUpdate.JobTitle;
      userJobInfo.Department = userJobInfoToUpdate.Department;
      return userJobInfo;
    }
    catch (Exception ex)
    {
      throw new Exception($"Couldn't update user job info: {ex.Message}");
    }
  }
}