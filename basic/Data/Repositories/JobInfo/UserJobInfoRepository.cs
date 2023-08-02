using basic.Models;
using Microsoft.EntityFrameworkCore;

namespace basic.Data.Repositories.JobInfo;
public class UserJobInfoRepository : IUserJobInfoRepository
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
     var values = await _context.UserJobInfo.ToListAsync();
      return values;
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
      var jobInfo = await _context.UserJobInfo.FindAsync(userJobInfoId) ?? throw new Exception($"Couldn't find user job info with id: {userJobInfoId}");
      jobInfo.JobTitle = userJobInfoToUpdate.JobTitle;
      jobInfo.Department = userJobInfoToUpdate.Department;
      return jobInfo;
    }
    catch (Exception ex)
    {
      throw new Exception($"Couldn't update user job info: {ex.Message}");
    }
  }
}