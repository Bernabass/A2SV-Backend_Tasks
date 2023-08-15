using BlogApp.Data;
using BlogApp.Start;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Connecting to database...");
var builder = new DbContextOptionsBuilder()
    .UseNpgsql("User Id=postgres;Password=postgres;Server=localhost;Port=5432;Database=BlogDb;Integrated Security=true,Pooling=true;");
Console.WriteLine("Connected to database.");

var context = new BlogDbContext(builder.Options);

var menu = new Menu(context);

menu.Start();