using Microsoft.EntityFrameworkCore;

namespace Simple_Blogging_App.Data
{
    public class BlogDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=localhost;Database=Blog_System;Username=postgres;Password=postgres");
        }
    }
}
