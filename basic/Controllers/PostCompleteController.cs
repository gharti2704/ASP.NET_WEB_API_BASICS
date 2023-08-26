using basic.Data.Repositories.Posts;
using basic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace basic.Controllers;

[Authorize]
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
            throw new Exception($"Error occurred: {e.Message}");
        }
    }

    [HttpGet("{postId:int:min(1)}")]
    public async Task<IActionResult> GetPost(int postId)
    {
        try
        {
            var post = await _completePostRepository.GetPost(postId);
            return post is not null ? Ok(post) : NotFound("Post doesn't Exist");
        }
        catch (Exception e)
        {
            throw new Exception($"Error occurred --Message: {e.Message}");
        }
    }
    
}