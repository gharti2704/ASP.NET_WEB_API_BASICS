using basic.Models;

namespace basic.Data.Repositories.Salary;
public class UserSalaryRepository : IUserSalaryRepository
{
    private readonly ApplicationDbContext _context;
    public UserSalaryRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<UserSalary>> GetUserSalaries()
    {
        try
        {
            return await Task.FromResult(_context.UserSalaries.ToList() as IEnumerable<UserSalary>);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<UserSalary> GetUserSalary(int userSalaryId)
    {
        try
        {
            return await _context.UserSalaries.FindAsync(userSalaryId) ?? throw new Exception($"Couldn't find user salary with id: {userSalaryId}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async void AddUserSalary(UserSalary userSalaryToAdd)
    {
        try
        {
           await _context.UserSalaries.AddAsync(userSalaryToAdd);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<UserSalary> DeleteUserSalary(int userSalaryId)
    {
        try
        {
            var userSalary = await _context.UserSalaries.FindAsync(userSalaryId) ?? throw new Exception($"Couldn't find user salary with id: {userSalaryId}");
            _context.UserSalaries.Remove(userSalary);
            return await Task.FromResult(userSalary);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<UserSalary> UpdateUserSalary(int userSalaryId, UserSalary userSalaryToUpdate)
    {
        try
        {
            var userSalary = await _context.UserSalaries.FindAsync(userSalaryId) ?? throw new Exception($"Couldn't find user salary with id: {userSalaryId}");
            userSalary.Salary = userSalaryToUpdate.Salary;
            return await Task.FromResult(userSalary);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}