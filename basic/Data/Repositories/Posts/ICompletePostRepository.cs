using basic.Models;

namespace basic.Data.Repositories.Posts;

public interface ICompletePostRepository
{
    Task<Post> GetPost(int postId);
    Task<IEnumerable<Post>> GetPosts();
}