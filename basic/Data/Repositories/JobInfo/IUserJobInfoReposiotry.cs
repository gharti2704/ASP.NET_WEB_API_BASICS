using basic.Models;

namespace basic.Data.Repositories.JobInfo;
public interface IUserJobInfoRepository
{
  Task<IEnumerable<Models.UserJobInfo>> GetUserJobInfos();
  Task<Models.UserJobInfo> GetUserJobInfo(int userJobInfoId);
  Task<Models.UserJobInfo> UpdateUserJobInfo(int userJobInfoId, Models.UserJobInfo userJobInfoToUpdate);

}