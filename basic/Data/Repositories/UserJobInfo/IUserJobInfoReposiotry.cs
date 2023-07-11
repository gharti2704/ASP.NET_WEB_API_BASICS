using basic.Models;

namespace basic.Data;
public interface IUserJobInfoReposiotry
{
  Task<IEnumerable<UserJobInfo>> GetUserJobInfos();
  Task<UserJobInfo> GetUserJobInfo(int userJobInfoId);
  Task<UserJobInfo> UpdateUserJobInfo(int userJobInfoId, UserJobInfo userJobInfoToUpdate);

}