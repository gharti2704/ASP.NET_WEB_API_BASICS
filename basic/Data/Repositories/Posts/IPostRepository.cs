using basic.Models;

namespace basic.Data.Repositories.Posts;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetPosts();
    Task<Post> GetPost(int userId);
    Task<Post> GetPostByUser(int userId);
    Task<Post> GetMyPost(int userId);
}