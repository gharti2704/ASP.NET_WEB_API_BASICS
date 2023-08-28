using basic.Data.Repositories.Posts;
using basic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace basic.Controllers;

// [Authorize]
[ApiController]
[Route("[controller]")]
public class PostCompleteController : ControllerBase
{
    private readonly ICompletePostRepository _completePostRepository;

    public PostCompleteController(ICompletePostRepository completePostRepository)
    {
        _completePostRepository = completePostRepository;
    }

    [HttpGet("posts")]
    public async Task<IActionResult> GetCompletePosts()
    {
        try
        {
            var posts=  await _completePostRepository.GetPosts();
            return Ok(posts);
        }
        catch (Exception e)
        {
            throw new Exception($"Error occurred --Message: {e.Message}");
        }
    }

    [HttpGet("{postId:int:min(1)}")]
    public async Task<IActionResult> GetPost(int postId)
    {
        try
        {
            var post = await _completePostRepository.GetPost(postId);
            return Ok(post);
        }
        catch (Exception e)
        {
            throw new Exception($"Error occurred --Message: {e.Message}");
        }
    }

    [HttpGet("search/{searchTerm}")]
    public async Task<IActionResult> Search(string searchTerm)
    {
        try
        {
            var posts = await _completePostRepository.Search(searchTerm);
            return Ok(posts);
        }
        catch (Exception e)
        {
            throw new Exception($"Error occurred --Message: {e.Message}");
        }
    }

    [HttpGet("user/{userId:int:min(1)}")]
    public async Task<IActionResult> GetPostByUser(int userId)
    {
        try
        {
            var posts = await _completePostRepository.GetPostByUser(userId);
            return Ok(posts);
        }
        catch (Exception e)
        {
            throw new Exception($"Error occurred --Message: {e.Message}");
        }
    }
    
}