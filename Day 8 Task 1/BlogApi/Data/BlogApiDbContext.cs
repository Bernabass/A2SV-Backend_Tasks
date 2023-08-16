using BlogApi.Models;
using Microsoft.EntityFrameworkCore;
namespace BlogApi.Data;

public class BlogApiDbContext : DbContext
{

    public DbSet<Post> Posts { get; set; }
    public DbSet <Comment> Comments { get; set; }

    public BlogApiDbContext(DbContextOptions<BlogApiDbContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasOne(p => p.Post)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.Id)
                .HasConstraintName("FK_Post_Id");

        });

        modelBuilder.Entity<Post>().Property(p => p.CreatedAt)
                    .HasDefaultValueSql("now()");

        modelBuilder.Entity<Comment>().Property(p => p.CreatedAt)
                    .HasDefaultValueSql("now()");
    }
}
