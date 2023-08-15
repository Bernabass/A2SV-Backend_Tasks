using BlogApp.Data;
using BlogApp.Models;
using BlogApp.Manager;
using System;

namespace BlogApp.Start
{
    public class Menu
    {
        private readonly PostManager _postManager;
        private readonly CommentManager _commentManager;
        private readonly Display _display;

        public Menu(BlogDbContext context)
        {
            _postManager = new PostManager(context);
            _commentManager = new CommentManager(context);
            _display = new Display(context);
        }

        private bool IsValidTitle(string name) => name.Length > 2;
        private bool IsValidContent(string input) => true; // Add more validation if needed
        private bool IsValidPostNumber(string input) => int.TryParse(input, out _);

        public string GetValidatedInput(string message, Func<string, bool> validator)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();

            while (string.IsNullOrEmpty(input) || !validator(input))
            {
                Console.WriteLine(string.IsNullOrEmpty(input) ? "You must input something. Try again." : "Invalid input. Try again.");
                input = Console.ReadLine();
            }

            return input;
        }

        private void AddPostFromInput()
        {
            string title = GetValidatedInput(
                "Enter post title",
                IsValidTitle
            );

            string content = GetValidatedInput(
                "Enter post content",
                IsValidContent
            );

            _postManager.Create(new Post { Title = title, Content = content });
            Console.WriteLine("Post created successfully");
        }

        private void AddCommentFromInput()
        {
            int postId;
            string postNumber = GetValidatedInput(
                "Enter post number",
                IsValidPostNumber
            );

            if (!int.TryParse(postNumber, out postId))
            {
                Console.WriteLine("Invalid post number");
                return;
            }

            string content = GetValidatedInput(
                "Enter comment text",
                IsValidContent
            );

            if (_postManager.GetById(postId) == null)
            {
                Console.WriteLine("Post not found");
                return;
            }

            _commentManager.Create(new Comment { PostId = postId, Text = content });
            Console.WriteLine("Comment added successfully");
        }


        private void PrintCommandList()
        {
            Console.WriteLine(@"
Available Commands:
    list      : List all posts.
    view      : View details of a post. Usage: view [post number] (e.g., 'view 2')
    add       : Add a new post.
    comment   : Add a comment to a post.
    delete    : Delete a post. Usage: delete [post number] (e.g., 'delete 1').
    delete c  : Delete a comment. Usage: delete c [comment number] (e.g., 'delete c 1').
    help      : Display this help menu.
    quit      : Quit the application.
");
        }

        private void DeleteComment(int commentId)
        {
            if (_commentManager.GetById(commentId) == null)
            {
                Console.WriteLine("Comment does not exist");
                return;
            }

            _commentManager.Delete(commentId);
            Console.WriteLine("Comment deleted");
        }

        private void DeletePost(int postId)
        {
            if (_postManager.GetById(postId) == null)
            {
                Console.WriteLine("Post not found");
                return;
            }

            _postManager.Delete(postId);
            Console.WriteLine("Post deleted successfully");
        }

        private void CommandHandler(string[] command)
        {
            switch (command[0].ToLower())
            {
                case "list":
                    _display.DisplayPosts();
                    break;
                case "add":
                    AddPostFromInput();
                    break;
                case "help":
                    PrintCommandList();
                    break;
                case "delete":
                    if (command.Length < 2)
                    {
                        Console.WriteLine("Delete arguments needed");
                        break;
                    }

                    if (command[1].ToLower() == "c" && command.Length >= 3)
                    {
                        if (int.TryParse(command[2], out int commentId))
                        {
                            DeleteComment(commentId);
                        }
                        else
                        {
                            Console.WriteLine("Invalid comment number");
                        }
                    }
                    else if (int.TryParse(command[1], out int postId))
                    {
                        DeletePost(postId);
                    }
                    else
                    {
                        Console.WriteLine("Invalid command");
                    }
                    break;
                case "view":
                    if (command.Length < 2)
                    {
                        Console.WriteLine("Post number needed");
                    }
                    else if (int.TryParse(command[1], out int postIdDetails))
                    {
                        _display.DisplayDetails(postIdDetails);
                    }
                    else
                    {
                        Console.WriteLine("Invalid post number");
                    }
                    break;
                case "comment":
                    AddCommentFromInput();
                    break;
                default:
                    Console.WriteLine("Invalid command");
                    break;
            }
        }

        public void Start()
        {
            Console.WriteLine("Welcome to the Blog App.");
            PrintCommandList();

            bool running = true;
            while (running)
            {
                string inputCommand = GetValidatedInput(
                    "\nEnter Command: ",
                    input => !string.IsNullOrWhiteSpace(input)
                );

                string[] splitInput = inputCommand.Split();
                if (splitInput[0].ToLower() == "quit")
                {
                    running = false;
                }
                else
                {
                    CommandHandler(splitInput);
                }
            }
        }
    }
}