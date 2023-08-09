using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Simple_Blogging_App.Data;

namespace Simple_Blogging_App.Manager
{
    public class PostManager
    {
        public void CreatePost(Post post)
        {
            if (string.IsNullOrWhiteSpace(post.Title))
                return;
            if (string.IsNullOrWhiteSpace(post.Content))
                return;
            
            using var dbContext = new BlogDbContext();
            post.CreatedAt = DateTime.UtcNow;
            dbContext.Posts.Add(post);
            dbContext.SaveChanges();
        }

        public List<Post> GetAllPosts()
        {
            using var dbContext = new BlogDbContext();
            return dbContext.Posts.Include(p => p.Comments).OrderByDescending(p => p.CreatedAt).ToList();
        }

        public Post GetPostById(int postId)
        {
            using var dbContext = new BlogDbContext();
            return dbContext.Posts.Include(p => p.Comments).SingleOrDefault(p => p.PostId == postId)!;
        }

        public void UpdatePost(Post post)
        {
            if (string.IsNullOrWhiteSpace(post.Title))
                return;
            if (string.IsNullOrWhiteSpace(post.Content))
                return;
                
            using var dbContext = new BlogDbContext();
            var existingPost = dbContext.Posts.Find(post.PostId);
            if (existingPost != null)
            {
                existingPost.Title = post.Title;
                existingPost.Content = post.Content;
                existingPost.CreatedAt = DateTime.UtcNow;
                dbContext.SaveChanges();
            }
        }

        public void DeletePost(int postId)
        {
            using var dbContext = new BlogDbContext();
            var postToDelete = dbContext.Posts.Find(postId);
            if (postToDelete != null)
            {
                dbContext.Posts.Remove(postToDelete);
                dbContext.SaveChanges();
            }
        }
    }
}
