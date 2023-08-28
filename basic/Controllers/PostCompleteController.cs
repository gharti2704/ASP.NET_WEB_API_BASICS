using AutoMapper;
using basic.Data.Repositories.Posts;
using basic.Dtos;
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
    private readonly IMapper _mapper;

    public PostCompleteController(ICompletePostRepository completePostRepository, IMapper mapper)
    {
        _completePostRepository = completePostRepository;
        _mapper = mapper;
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
    public async Task<IActionResult> GetCompletePost(int postId)
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

    [HttpPost("post")]
    public IActionResult CreatePost(PostToAddDto post)
    {
        try
        {
            var postToCreate = _mapper.Map<Post>(post);
            postToCreate.UserId = int.Parse(this.User.FindFirst("userId")!.Value);
            postToCreate.PostCreated = DateTime.Now;
            postToCreate.PostUpdated = DateTime.Now;
            _completePostRepository.CreatePost(postToCreate);
            return CreatedAtAction(nameof(GetCompletePost), new {postId = postToCreate.PostId}, postToCreate);
        }
        catch (Exception e)
        {
            throw new Exception($"Error occurred --Message: {e.Message}");
        }
    }

    [HttpPut("update")]
    public IActionResult UpdatePost(PostToEditDto post)
    {
        try
        {
            var postToUpdate = _mapper.Map<Post>(post);
            postToUpdate.PostContent = post.PostContent;
            postToUpdate.PostTitle = post.PostTitle;
            postToUpdate.PostUpdated = DateTime.Now;

            return Ok(_completePostRepository.UpdatePost(postToUpdate));
        }
        catch (Exception e)
        {
            throw new Exception($"Error occurred --Message: {e.Message}");
        }
    }

    [HttpDelete("delete/{postId:int:min(1)}")]
    public IActionResult DeletePost(int postId)
    {
        try
        {
            var userId = int.Parse(this.User.FindFirst("userId")!.Value);
            return Ok(_completePostRepository.DeletePost(postId, userId));
        }
        catch (Exception e)
        {
            throw new Exception($"Error occurred --Message: {e.Message}");
        }
    }
    
}