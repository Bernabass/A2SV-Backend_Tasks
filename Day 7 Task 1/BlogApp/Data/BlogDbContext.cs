using BlogApp.Models;
using Microsoft.EntityFrameworkCore;
namespace BlogApp.Data;

public class BlogDbContext : DbContext
{

    public DbSet<Post> Posts { get; set; }
    public DbSet <Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(@"Host=localhost;Database=BlogDb;Username=postgres;Password=postgres");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasOne(p => p.Post)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.PostId)
                .HasConstraintName("FK_Post_Id");

        });

        modelBuilder.Entity<Post>().Property(p => p.CreatedAt)
                    .HasDefaultValueSql("now()");

        modelBuilder.Entity<Comment>().Property(p => p.CreatedAt)
                    .HasDefaultValueSql("now()");
    }
}
