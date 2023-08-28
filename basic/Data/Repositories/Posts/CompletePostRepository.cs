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

    public async Task<IEnumerable<Post>> Search(string searchTerm)
    {
        try
        {
            if (_context.Posts is null) throw new Exception("Posts context is null");
            return await _context.Posts.FromSqlInterpolated($"EXEC BasicWebAPI.spPosts_Get @SearchValue={searchTerm}")
                .ToListAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<IEnumerable<Post>> GetPostByUser(int userId)
    {
        try
        {
            if (_context.Posts is null) throw new Exception("Posts context is null");
            var posts = await _context.Posts.FromSqlInterpolated($"EXEC BasicWebAPI.spPosts_Get @UserId={userId}")
                .ToListAsync();
            return posts;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public string UpdatePost(Post post)
    {
        try
        {
            if (_context.Posts is null) throw new Exception("Database context is null");
            var postCreated = _context.Posts.FromSqlInterpolated(
                $"EXEC BasicWebAPI.spPosts_Upsert @UserId={post.UserId}, @PostTitle={post.PostTitle}, @PostContent={post.PostContent}, @PostId={post.PostId}"
            );
            return "Post updated successfully";
        }
        catch (Exception e)
        {
            throw new Exception($"Error occurred --Message: {e.Message}");
        }
    }
    
    public string CreatePost(Post post)
    {
        try
        {
            if (_context.Posts is null) throw new Exception("Database context is null");
            var postCreated = _context.Posts.FromSqlInterpolated(
                $"EXEC BasicWebAPI.spPosts_Upsert @UserId={post.UserId}, @PostTitle={post.PostTitle}, @PostContent={post.PostContent}, @PostId={post.PostId}"
            );
            return "Post created successfully";
        }
        catch (Exception e)
        {
            throw new Exception($"Error occurred --Message: {e.Message}");
        }
    }

    public string DeletePost(int postId, int userId)
    {
        try
        {
            if (_context.Posts is null) throw new Exception("Database context is null");
            _context.Posts.FromSqlInterpolated($"EXEC BasicWebAPI.spPost_Delete @PostId={postId}, @UserId={userId}");
            return "Post deleted successfully";
        }
        catch (Exception e)
        {
            throw new Exception($"Error occurred --Message: {e.Message}");
        }
    }
}