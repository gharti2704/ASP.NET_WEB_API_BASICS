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
    public async void AddEntity<T>(T entityToAdd) where T : class
    {
        try
        {
            await _context.AddAsync(entityToAdd);
        }
        catch (Exception ex)
        {
            throw new Exception($"Couldn't add entity: {ex.Message}");
        }
    }

    public void DeleteEntity<T>(T entityToDelete) where T : class
    {
        try
        {
            _context.Remove(entityToDelete);
        }
        catch (Exception ex)
        {
            throw new Exception($"Couldn't delete entity: {ex.Message}");
        }
    }

    public Task<UserJobInfo> GetUserJobInfo(int userJobInfoId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserJobInfo>> GetUserJobInfos()
    {
        throw new NotImplementedException();
    }

    public Task<bool> SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<T> UpdateEntity<T>(int UserJobInfoId, T entityToUpdate) where T : class
    {
        throw new NotImplementedException();
    }
}