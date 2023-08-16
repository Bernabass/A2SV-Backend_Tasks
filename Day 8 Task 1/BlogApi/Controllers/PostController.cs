using BlogApi.Data;
using BlogApi.Models;
using BlogApp.Manager;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly PostManager _postManager;
    
    public PostsController(BlogApiDbContext context)
    {
        _postManager = new PostManager(context);
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_postManager.GetAll());
    }

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        Post? post = _postManager.GetById(id);

        if (post is null)
            return NotFound("Post Not Found");

        return Ok(post);
    }

    [HttpPost]
    public IActionResult Post(PostContents postContents)
    {
        try
        {
            if (postContents.Title.Length < 1)
                return BadRequest("Title can't be empty");
            if (postContents.Content.Length < 1)
                return BadRequest("Content can't be empty");

            var createdPost = _postManager.Create(new Post() {Title = postContents.Title, Content = postContents.Content});
            return CreatedAtAction("Get", createdPost.Id, createdPost);
        }
        catch (Exception)
        {
            return StatusCode(500, "Server Error");
        }
    }

    [HttpPatch]
    public IActionResult Patch(int id, PostContents postContents)
    {
        try
        {
            string? newTitle = null;
            string? newContent = null;
            if (postContents.Title.Length >= 1)
                newTitle = postContents.Title;
            if (postContents.Content.Length >= 1)
                newContent = postContents.Content;

            var updatedPost = _postManager.Update(id, newTitle, newContent);

            return Ok(updatedPost);
        }
        catch (InvalidOperationException)
        {
            return BadRequest("Post does not exist");
        }
        catch (Exception)
        {
            return StatusCode(500, "Server Error");
        }
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        try
        {
            _postManager.Delete(id);

            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return BadRequest("Post does not exist");
        }
        catch (Exception)
        {
            return StatusCode(500, "Server Error");
        }
    }
}