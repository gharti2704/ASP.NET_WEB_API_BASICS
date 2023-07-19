using basic.Models;
namespace basic.Data.Repositories.Salary;
public interface IUserSalaryRepository
{
 Task<IEnumerable<UserSalary>> GetUserSalaries();
 Task<UserSalary> GetUserSalary(int userSalaryId);
 Task<Models.UserSalary> UpdateUserSalary(int userSalaryId, UserSalary userSalaryToUpdate);
 void AddUserSalary(UserSalary userSalaryToAdd);
 Task<UserSalary> DeleteUserSalary(int userSalaryId);
}