using BlogApp.Data;
using BlogApp.Start;
using Microsoft.EntityFrameworkCore;

var menu = new Menu(new BlogDbContext());

menu.Start();