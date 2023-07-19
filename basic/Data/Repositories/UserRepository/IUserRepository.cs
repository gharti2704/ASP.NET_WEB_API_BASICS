using basic.Models;

namespace basic.Data.Repositories.UserRepository;
public interface IUserRepository
{
  Task<IEnumerable<User>> GetUsers();
  Task<User> GetUser(int userId);
  Task<User> UpdateUser(int userId, User userToUpdate);
}