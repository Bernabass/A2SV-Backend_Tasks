using BlogApi.Data;
using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Manager;

public class PostManager
{
    private readonly BlogApiDbContext _context;

    public PostManager(BlogApiDbContext context)
    {
        _context = context;
    }

    public Post? Create(Post post)
    {
        _context.Posts.Add(post);
        _context.SaveChanges();

        return post;
    }

    public IEnumerable<Post> GetAll()
    {
        return _context.Posts.AsNoTracking().ToList();
    }

    public Post? GetById(int id)
    {
        return _context.Posts.Find(id);
    }
   public Post Update(int id, string? title=null, string? content=null)
    {
        Post? post = _context.Posts.Find(id);

        if (post is null)
        {
            throw new InvalidOperationException("Post does not exist");
        }

        post.Title = title ?? post.Title;
        post.Content = content ?? post.Content;
        _context.SaveChanges();

        return post;
    }

    public void Delete(int id)
    {
        Post? post = _context.Posts.Find(id);
        if (post is null)
            return;
        
        _context.Posts.Remove(post);
        _context.SaveChanges();
    }

}