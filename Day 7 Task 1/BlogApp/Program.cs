using BlogApp.Data;
using BlogApp.Start;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Connecting to database...");

Console.WriteLine("Connected to database.");

var context = new BlogDbContext();

var menu = new Menu(context);

menu.Start();