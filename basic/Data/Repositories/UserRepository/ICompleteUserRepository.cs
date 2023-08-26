using basic.Models;

namespace basic.Data.Repositories.UserRepository;

public interface ICompleteUserRepository
{
    Task<IEnumerable<UserComplete>> GetCompleteUsers();
    Task<UserComplete> GetCompleteUser(int userId);
    string UpdateCompleteUser(UserComplete user);
    string DeleteCompleteUser(int userId);
}