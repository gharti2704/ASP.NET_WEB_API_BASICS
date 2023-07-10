using basic.Models;
using Microsoft.EntityFrameworkCore;

namespace basic.Data;
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

    public async Task<T> UpdateEntity<T>(int UserId, T entityToUpdate) where T : class
    {
        try
        {
            var userToUpdate = await _context.Users.FindAsync(UserId) ?? throw new Exception("User not found");

            return entityToUpdate;
        }
        catch (Exception ex)
        {
            throw new Exception($"Couldn't update entity: {ex.Message}");
        }
    }

    public async Task<bool> SaveChangesAsync()
    {
        try
        {
            return await _context.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            throw new Exception($"Couldn't save changes: {ex.Message}");
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
}