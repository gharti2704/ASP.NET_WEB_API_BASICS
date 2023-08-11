using AutoMapper;
using basic.Data;
using basic.Data.Repositories.Common;
using basic.Data.Repositories.Posts;
using basic.Dtos;
using basic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace basic.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostRepository _postRepository;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICommonRepository _commonRepository;

    public PostController(IPostRepository postRepository, ApplicationDbContext context, IMapper mapper, ICommonRepository commonRepository)
    {
        _postRepository = postRepository;
        _context = context;
        _mapper = mapper;
        _commonRepository = commonRepository;
    }

    [HttpGet("posts")]
    public async Task<ActionResult<Post>> GetPosts()
    {
        try
        {
            var posts = await _postRepository.GetPosts();
            return Ok(posts);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    // Ensure that the postId is a positive number
    [HttpGet("{postId:int:min(1)}")]
    public async Task<ActionResult<Post>> GetPost(int postId)
    {
        try
        {
            var post = await _postRepository.GetPost(postId);
            return Ok(post);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    } 
    
    [HttpGet("postByUser/{userId:int:min(1)}")]
    public async Task<ActionResult<Post>> GetPostByUser(int userId)
    {
        try
        {
            var post = await _postRepository.GetPostByUser(userId);
            return Ok(post);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    [HttpGet("myPost")]
    public async Task<ActionResult<Post>> GetMyPost()
    {
        try
        {
            var userId = int.Parse(this.User.FindFirst("userId")!.Value);
            var post = await _postRepository.GetMyPost(userId);
            return Ok(post);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpPost("post")]
    public async Task<ActionResult<Post>> CreatePost(PostToAddDto post)
    {
        var postToCreate = _mapper.Map<Post>(post);
        postToCreate.UserId = int.Parse(this.User.FindFirst("userId").Value);
        postToCreate.PostCreated = DateTime.Now;
        postToCreate.PostUpdated = DateTime.Now;

        try
        {
            _commonRepository.AddEntity<Post>(postToCreate);
            await _commonRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPost), new { postId = postToCreate.PostId }, postToCreate);
        }
        catch (Exception e)
        {
            
            throw new Exception(e.Message);
        }
    }

    [HttpPut("post")]
    public async Task<ActionResult<Post>> EditPost(PostToEditDto post)
    {
        try
        {
            var postToEdit = await _context.Posts.FirstOrDefaultAsync(p =>
                p.PostId == post.PostId && p.UserId.ToString() == this.User.FindFirst("userId").Value);
            if (postToEdit is null) throw new Exception("Post not found");
            postToEdit.PostContent = post.PostContent;
            postToEdit.PostTitle = post.PostTitle;
            postToEdit.PostUpdated = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok(postToEdit);
        }
        catch (Exception e)
        {
            
            throw new Exception(e.Message);
        }
    }

    [HttpDelete("post/{postId:int:min(1)}")]
    public async Task<ActionResult<Post>> DeletePost(int postId)
    {
        try
        {
            var postToDelete = await _postRepository.GetPost(postId);
           _commonRepository.DeleteEntity<Post>(postToDelete);
           await _commonRepository.SaveChangesAsync();
           return Ok("Successfully deleted the post");
        }
        catch (Exception e)
        {
            
            throw new Exception(e.Message);
        }
    }
}