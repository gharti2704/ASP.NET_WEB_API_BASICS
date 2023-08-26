using basic.Models;
using Microsoft.EntityFrameworkCore;


namespace basic.Data.Repositories.UserRepository;

public class CompleteUserRepository : ICompleteUserRepository
{
    private readonly ApplicationDbContext _context;
    public CompleteUserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<UserComplete>> GetCompleteUsers()
    {
        try
        {
            return await _context.UsersComplete.FromSqlInterpolated($"EXEC BasicWebAPI.spUsers_Get").ToListAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    public async Task<UserComplete> GetCompleteUser(int userId)
    {
        try
        {
            return (await _context.UsersComplete.FromSqlInterpolated($"EXEC BasicWebAPI.spUsers_Get @UserId={userId}")
                .ToListAsync())[0] ?? throw new Exception("User not found");
        }
        catch (Exception ex)
        {
            throw new Exception($"Couldn't find user: {ex.Message}");
        }
    }
    
    public string UpdateCompleteUser(UserComplete user)
    {
        try
        {
            _context.UsersComplete.FromSqlInterpolated(
                $"EXEC BasicWebAPI.spUser_Upsert @FirstName={user.FirstName}, @LastName={user.LastName}, @Email={user.Email}, @Gender={user.Gender}, @JobTitle={user.JobTitle}, @Department={user.Department}, @Salary={user.Salary}, @Active={user.Active}, @UserId={user.UserId}");
            return "User updated successfully";
        }
        catch (Exception ex)
        {
            throw new Exception($"Couldn't update entity: {ex.Message}");
        }
    }
    
    public string DeleteCompleteUser(int userId)
    {
        try
        {
            _context.UsersComplete.FromSqlInterpolated($"EXEC BasicWebAPI.spUser_Delete @UserId={userId}");
            return "User deleted successfully";
        }
        catch (Exception ex)
        {
            throw new Exception($"Couldn't delete entity: {ex.Message}");
        }
    }
    
}