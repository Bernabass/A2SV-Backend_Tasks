using BlogApi.Data;
using BlogApi.Models;
using BlogApi.Manager;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly CommentManager _commentManager;

    public CommentsController(BlogApiDbContext context)
    {
        _commentManager = new CommentManager(context);
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_commentManager.GetAll());
    }
    
    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        Comment? comment = _commentManager.GetById(id);

        if (comment is null)
            return NotFound("Comment does not exist");

        return Ok(comment);
    }


    [HttpPost]
    public IActionResult Create(CommentContent content)
    {
        try
        {
            Comment comment = _commentManager.Create(new Comment() { Id = content.PostId, Text = content.Text });
            comment.Post = null;
            return CreatedAtAction("Get", comment.Id, comment);
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

    [HttpPatch]
    public IActionResult Patch(int id, CommentContent contents)
    {
        try
        {
            var comment = _commentManager.Update(id, contents.Text);
            return Ok(comment);
        }
        catch (InvalidOperationException)
        {
            return BadRequest("Comment does not exist");
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
            _commentManager.Delete(id);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return BadRequest("Comment does not exist");
        }
        catch (Exception)
        {
            return StatusCode(500, "Server Error");
        }
    }
}