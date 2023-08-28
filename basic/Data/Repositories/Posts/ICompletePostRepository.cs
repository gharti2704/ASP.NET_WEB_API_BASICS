using basic.Models;

namespace basic.Data.Repositories.Posts;

public interface ICompletePostRepository
{
    Task<Post> GetPost(int postId);
    Task<IEnumerable<Post>> GetPosts();
    Task<IEnumerable<Post>> Search(string searchTerm);
    Task<IEnumerable<Post>> GetPostByUser(int userId);
    string UpdatePost(Post post);
    string CreatePost(Post post);
    string DeletePost(int postId, int userId);
}