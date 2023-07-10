using basic.Models;

namespace basic.Data;
public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsers();
    Task<User> GetUser(int userId);
    void AddEntity<T>(T entityToAdd) where T : class;
    Task<T> UpdateEntity<T>(int UserId, T entityToUpdate) where T : class;
    Task<bool> SaveChangesAsync();
    void DeleteEntity<T>(T entityToDelete) where T : class;

}