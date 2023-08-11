using basic.Models;
using Microsoft.EntityFrameworkCore;

namespace basic.Data.Repositories.Posts;

public class PostRepository : IPostRepository
{
    private readonly ApplicationDbContext _context;
    public PostRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Post>> GetPosts()
    {
        try
        {
            return await _context.Posts.ToListAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    public async Task<Post> GetPost(int id)
    {
        try
        {
            return await _context.Posts.FindAsync(id);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    } 
    
    public async Task<Post> GetPostByUser(int id)
    {
        try
        {
            return await _context.Posts.FirstOrDefaultAsync(p => p.UserId == id);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    } 
    
    public async Task<Post> GetMyPost(int id)
    {
        try
        {
            return await _context.Posts.FirstOrDefaultAsync(p => p.UserId == id);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}