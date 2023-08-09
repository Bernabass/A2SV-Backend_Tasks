using System;
using Simple_Blogging_App.Data;
using Simple_Blogging_App.Manager;

namespace Simple_Blogging_App.Menu
{
    public class Menu
    {
        public PostManager postManager;
        public CommentManager commentManager;

        public Menu()
        {
            postManager = new PostManager();
            commentManager = new CommentManager();
        }
        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Simple Blogging Application");
                Console.WriteLine("1. Create Post");
                Console.WriteLine("2. Open Post");
                Console.WriteLine("3. View All Posts");
                Console.WriteLine("4. Edit Post");
                Console.WriteLine("5. Delete Post");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreatePost();
                        break;

                    case "2":
                        OpenPost();
                        break;

                    case "3":
                        ViewAllPosts();
                        Console.WriteLine();
                        Console.Write("Press Enter to continue...");
                        Console.ReadLine();
                        break;

                    case "4":
                        EditPost();
                        break;

                    case "5":
                        DeletePost();
                        break;

                    case "6":
                        Console.WriteLine("See you soon, take care!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        Console.Write("Press Enter to continue...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void CreatePost()
        {
            Console.Write("Enter post title: ");
            var title = Console.ReadLine()!;

            while (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Post title cannot be empty.");
                Console.Write("Enter post title: ");
                title = Console.ReadLine()!;
            }
            Console.Write("Enter post content: ");
            var content = Console.ReadLine()!;

            while (string.IsNullOrWhiteSpace(content))
            {
                Console.WriteLine("Post content cannot be empty.");
                Console.Write("Enter post content: ");
                content = Console.ReadLine()!;
            }

            var newPost = new Post { Title = title, Content = content };
            postManager.CreatePost(newPost);
            Console.WriteLine("Post created successfully.");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }



        private void OpenPost()
        {
            var posts = postManager.GetAllPosts();

            if (posts == null || posts.Count == 0)
            {
                Console.WriteLine("No posts found. Please add some posts first.");
                Console.WriteLine();
                Console.Write("Press Enter to continue...");
                Console.ReadLine();
                return;
            }
            
            Console.Clear();
            ViewAllPosts();
            Console.WriteLine("Opening a Post");
            Console.WriteLine("------------------");

            int postId;
            Console.Write("Enter post ID to open: ");

            while (!int.TryParse(Console.ReadLine(), out postId) && postId <= 0)
            {
                Console.WriteLine("Invalid post ID.");
                Console.Write("Enter post ID to open: ");
            }
            

            var postToOpen = postManager.GetPostById(postId);
            if (postToOpen != null)
            {
                while(true){

                    ViewAPost(postId);
                    Console.WriteLine();
                    Console.WriteLine("1. Add Comment");
                    Console.WriteLine("2. Edit Comment");
                    Console.WriteLine("3. Delete Comment");
                    Console.WriteLine("4. View All Comments");
                    Console.WriteLine("5. Back to Posts");
                    Console.Write("Choose an option: ");
                    var commentOption = Console.ReadLine();

                    switch (commentOption)
                    {
                        case "1":
                            AddComment(postId);
                            break;
                        case "2":
                            EditComment(postId);
                            break;
                        case "3":
                            DeleteComment(postId);
                            break;
                        case "4":
                            ViewAllComments(postId);
                            Console.WriteLine();
                            Console.Write("Press Enter to continue...");
                            Console.ReadLine();
                            break;
                        case "5":
                            return;
                        default:
                            Console.WriteLine("Invalid option.");
                            Console.WriteLine();
                            Console.Write("Press Enter to continue...");
                            Console.ReadLine();
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Post not found.");
                Console.WriteLine();
                Console.Write("Press Enter to continue...");
                Console.ReadLine();
            }
        }
           

        private void ViewAllPosts()
        {
            var posts = postManager.GetAllPosts();

            if (posts == null || posts.Count == 0)
            {
                Console.WriteLine("No posts found.");
                
                return;
            }
            
            Console.Clear();
            Console.WriteLine("\t\t\t    ------------------");
            Console.WriteLine("\t\t\t        All Posts");
            Console.WriteLine("\t\t\t    ------------------");

            string separator = new string('-', 75);
            Console.WriteLine(separator);
            Console.WriteLine($"{"Post_Id", -10}{"Title", -20}{"Content", -25}{"Last Updated", -20}");
            Console.WriteLine(separator);

            foreach (var post in posts)
            {
                string contents = post.Content?.Length > 20 ? post.Content.Substring(0, 17) + "..." : post.Content ?? "N/A";
                Console.WriteLine($"{post.PostId, -10}{post.Title, -20}{contents, -25}{post.CreatedAt, -20}");
                Console.WriteLine(separator);
            }
            Console.WriteLine();
        }

        private void ViewAPost(int postId)
        {
            Console.Clear();
            Console.WriteLine("\t\t\t    ------------------");
            Console.WriteLine("\t\t\t        Current Post");
            Console.WriteLine("\t\t\t    ------------------");

            string separator = new string('-', 75);
            Console.WriteLine(separator);
            Console.WriteLine($"{"Post_Id", -10}{"Title", -20}{"Content", -25}{"Last Updated", -20}");
            Console.WriteLine(separator);

            var post = postManager.GetPostById(postId);
            string contents = post.Content?.Length > 20 ? post.Content.Substring(0, 17) + "..." : post.Content ?? "N/A";
            Console.WriteLine($"{post.PostId, -10}{post.Title, -20}{contents, -25}{post.CreatedAt, -20}");
            Console.WriteLine(separator);
            Console.WriteLine();
        }

        private void EditPost()
        {
            Console.Clear();
            ViewAllPosts();
            Console.WriteLine("Editing a Post");
            Console.WriteLine("------------------");

            Console.Write("Enter post ID to edit: ");
            string postId = Console.ReadLine()!;

            while (string.IsNullOrWhiteSpace(postId))
            {
                Console.WriteLine("Post ID cannot be empty.");
                Console.Write("Enter post ID to edit: ");
                postId = Console.ReadLine()!;
            }

            if (int.TryParse(postId, out int postIdInt) && postIdInt > 0)
            {
                var postToEdit = postManager.GetPostById(postIdInt);
                if (postToEdit != null)
                {
                    Console.Write("Enter new post title: ");
                    var newTitle = Console.ReadLine()!;

                    while (string.IsNullOrWhiteSpace(newTitle))
                    {
                        Console.WriteLine("Post title cannot be empty.");
                        Console.Write("Enter post title: ");
                        newTitle = Console.ReadLine()!;
                    }

                    Console.Write("Enter new post content: ");
                    var newContent = Console.ReadLine()!;

                    while (string.IsNullOrWhiteSpace(newContent))
                    {
                        Console.WriteLine("Post content cannot be empty.");
                        Console.Write("Enter post content: ");
                        newContent = Console.ReadLine()!;
                    }

                    postToEdit.Title = newTitle;
                    postToEdit.Content = newContent;

                    postManager.UpdatePost(postToEdit);
                    Console.WriteLine("Post updated successfully.");
                }
                else
                    Console.WriteLine("Post not found.");
            }
            
            else
                Console.WriteLine("Invalid post ID.");
            
            Console.WriteLine();
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
        }

        private void DeletePost()
        {
            Console.Clear();
            ViewAllPosts();
            Console.WriteLine("Deleting a Post");
            Console.WriteLine("------------------");

            Console.Write("Enter post ID to delete: ");
            string postId = Console.ReadLine()!;

            while (string.IsNullOrWhiteSpace(postId))
            {
                Console.WriteLine("Post ID cannot be empty.");
                Console.Write("Enter post ID to delete: ");
                postId = Console.ReadLine()!;
            }

            if (int.TryParse(postId, out int postIdInt) && postIdInt > 0)
            {
                var deletedPost = postManager.GetPostById(postIdInt);
                if (deletedPost != null)
                {
                    postManager.DeletePost(postIdInt);
                    Console.WriteLine("Post deleted successfully.");
                }
                else
                    Console.WriteLine("Post not found.");   
            }
            else
                Console.WriteLine("Invalid post ID.");
            
            Console.WriteLine();
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
        }


        private void AddComment(int postId)
        {
            Console.Clear();
            Console.WriteLine("Adding a Comment");
            Console.WriteLine("------------------");

            Console.Write("Enter comment text: ");
            var commentText = Console.ReadLine()!;

            while (string.IsNullOrWhiteSpace(commentText))
            {
                Console.WriteLine("Comment text cannot be empty.");
                Console.Write("Enter comment text: ");
                commentText = Console.ReadLine()!;
            }

            var newComment = new Comment { Text = commentText, PostId = postId };
            commentManager.CreateComment(newComment);
            Console.WriteLine("Comment added successfully.");

            Console.WriteLine();
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
        }


        private void EditComment(int postId)
        {
            Console.Clear();
            ViewAllComments(postId);
            Console.WriteLine("Editing a Comment");
            Console.WriteLine("------------------");

            Console.Write("Enter comment index to edit: ");
            string editIndex = Console.ReadLine()!;

            while (string.IsNullOrWhiteSpace(editIndex))
            {
                Console.WriteLine("Comment index cannot be empty.");
                Console.Write("Enter comment index to edit: ");
                editIndex = Console.ReadLine()!;
            }

            if (int.TryParse(editIndex, out int editIndexInt) && editIndexInt > 0)
            {
                var commentToEdit = commentManager.GetCommentByPost(postId, editIndexInt);
                if (commentToEdit == null)
                {
                    Console.WriteLine("Comment index not found.");
                    return;
                }

                Console.Write("Enter new comment text: ");
                var newCommentText = Console.ReadLine()!;

                while (string.IsNullOrWhiteSpace(newCommentText))
                {
                    Console.WriteLine("Comment text cannot be empty.");
                    Console.Write("Enter comment text: ");
                    newCommentText = Console.ReadLine()!;
                }
                
                commentToEdit.Text = newCommentText;
                commentManager.UpdateComment(commentToEdit);
                Console.WriteLine("Comment updated successfully.");
            }
            else
                Console.WriteLine("Invalid comment index.");

            Console.WriteLine();
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
        }


        private void DeleteComment(int postId)
        {
            Console.Clear();
            ViewAllComments(postId);
            Console.WriteLine("Deleting a Comment");
            Console.WriteLine("------------------");

            Console.Write("Enter comment index to delete: ");
            string delIndex = Console.ReadLine()!;

            while (string.IsNullOrWhiteSpace(delIndex))
            {
                Console.WriteLine("Comment index cannot be empty.");
                Console.Write("Enter comment index to delete: ");
                delIndex = Console.ReadLine()!;
            }

            if (int.TryParse(delIndex, out int delIndexInt) && delIndexInt > 0)
            {
                var commentToDelete = commentManager.GetCommentByPost(postId, delIndexInt);
                if (commentToDelete == null)
                {
                    Console.WriteLine("Comment index not found.");
                    Console.WriteLine();
                    Console.Write("Press Enter to continue...");
                    Console.ReadLine();
                    return;
                }
            
                commentManager.DeleteComment(commentToDelete.CommentId);
                Console.WriteLine("Comment deleted successfully.");
            }
            else
                Console.WriteLine("Invalid comment index.");

            Console.WriteLine();
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
        }


        private void ViewAllComments(int postId)
        {
            var comments = commentManager.GetAllComments(postId);

            if (comments == null)
            {
                Console.WriteLine("No comments found.");
                Console.WriteLine();
                Console.Write("Press Enter to continue...");
                Console.ReadLine();
                return;
            }

            Console.Clear();
            Console.WriteLine("\t\t\t    ------------------");
            Console.WriteLine("\t\t\t        All Comments");
            Console.WriteLine("\t\t\t    ------------------");

            string separator = new string('-', 80);
            Console.WriteLine(separator);
            Console.WriteLine($"{"Comment_Id", -15}{"Post_Id", -20}{"Content", -25}{"Last Updated", -20}");
            Console.WriteLine(separator);

            
            
            foreach (var comment in comments)
            {
                string contents = comment.Text?.Length > 20 ? comment.Text.Substring(0, 17) + "..." : comment.Text ?? "N/A";
                Console.WriteLine($"{comment.CommentId, -15}{comment.PostId, -20}{contents, -25}{comment.CreatedAt, -20}");
                Console.WriteLine(separator);
            }
            Console.WriteLine();
        }  
    }
}
