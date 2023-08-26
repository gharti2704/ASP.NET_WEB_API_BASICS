using basic.Models;
using Microsoft.EntityFrameworkCore;

namespace basic.Data.Repositories.Posts;

public class CompletePostRepository : ICompletePostRepository
{
    private readonly ApplicationDbContext _context;

    public CompletePostRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Post> GetPost(int postId)
    {
        try
        {
            if (_context.Posts is null) throw new Exception("Database context is null");
            var posts = await _context.Posts.FromSqlInterpolated($"EXEC BasicWebAPI.spPosts_Get @PostId={postId}").ToListAsync();
            var post = posts.FirstOrDefault();
            if (post is null) throw new Exception("Post not found");
            return post;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<IEnumerable<Post>> GetPosts()
    {
        try
        {
            if (_context.Posts is null) throw new Exception("Database context is null");
            return await _context.Posts.FromSqlInterpolated($"EXEC BasicWebAPI.spPosts_Get").ToListAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}