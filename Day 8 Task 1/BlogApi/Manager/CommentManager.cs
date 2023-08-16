using BlogApi.Data;
using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Manager;

public class CommentManager
{
    private readonly BlogApiDbContext _context;

    public CommentManager(BlogApiDbContext context)
    {
        _context = context;
    }

    public Comment Create(Comment comment)
    {
        _context.Comments.Add(comment);
        _context.SaveChanges();

        return comment;
    }

    public IEnumerable<Comment> GetAll()
    {
        return _context.Comments.AsNoTracking().ToList();
    }

    public Comment? GetById(int id)
    {
        return _context.Comments.Find(id);
    }


    public Comment Update(int id, string text)
    {
        Comment? comment = _context.Comments.Find(id);

        if (comment is null)
        {
            throw new InvalidOperationException("Comment doesn't exist!");

        }

        comment.Text = text ?? comment.Text;
        _context.SaveChanges();

        return comment;
    }
      public void Delete(int id)
    {
        Comment? comment = _context.Comments.Find(id);
        if (comment is null)
            return;

        _context.Comments.Remove(comment);
        _context.SaveChanges();
    }  
}