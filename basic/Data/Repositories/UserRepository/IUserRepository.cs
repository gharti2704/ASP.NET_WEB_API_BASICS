using basic.Models;

namespace basic.Data;
public interface IUserRepository
{
  Task<IEnumerable<User>> GetUsers();
  Task<User> GetUser(int userId);
  Task<User> UpdateUser(int userId, User userToUpdate);
}