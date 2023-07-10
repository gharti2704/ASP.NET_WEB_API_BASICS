using basic.Models;

namespace basic.Data;
public interface IUserJobInfoReposiotry
{
    Task<IEnumerable<UserJobInfo>> GetUserJobInfos();
    Task<UserJobInfo> GetUserJobInfo(int userJobInfoId);
    void AddEntity<T>(T entityToAdd) where T : class;
    Task<T> UpdateEntity<T>(int UserJobInfoId, T entityToUpdate) where T : class;
    Task<bool> SaveChangesAsync();
    void DeleteEntity<T>(T entityToDelete) where T : class;

}