using basic.Models;
using Microsoft.EntityFrameworkCore;

namespace basic.Data.Repositories.UserRepository;
public class UserRepository : IUserRepository
{
  private readonly ApplicationDbContext _context;
  public UserRepository(ApplicationDbContext context)
  {
    _context = context;
  }
  public async Task<IEnumerable<User>> GetUsers()
  {
    return await _context.Users.ToListAsync();
  }
  public async Task<User> GetUser(int userId)
  {
    try
    {
      return await _context.Users.FindAsync(userId) ?? throw new Exception("User not found");
    }
    catch (Exception ex)
    {
      throw new Exception($"Couldn't find user: {ex.Message}");
    }
  }
  public async Task<User> UpdateUser(int userId, User userToUpdate)
  {
    try
    {
      var user = await _context.Users.FindAsync(userId) ?? throw new Exception("User not found");
      user.Active = userToUpdate.Active;
      user.FirstName = userToUpdate.FirstName;
      user.LastName = userToUpdate.LastName;
      user.Email = userToUpdate.Email;
      return user;
    }
    catch (Exception ex)
    {
      throw new Exception($"Couldn't update entity: {ex.Message}");
    }
  }
}