using BlogApp.Data;
using BlogApp.Manager;

namespace BlogApp.Start
{
    public class Display
    {
        private PostManager _postManager;
        private CommentManager _commentManager;

        public Display(BlogDbContext context)
        {
            _postManager = new PostManager(context);
            _commentManager = new CommentManager(context);
        }

        public void DisplayPosts()
        {
            Console.WriteLine("    ------------- Posts --------------");
            foreach (var post in _postManager.GetAll())
            {
                Console.WriteLine($@"
            ----------------------------------
            Title: {post.Title}
            Content: {post.Content}
            Post ID: #{post.Id}
            Created At: {post.CreatedAt}
            ----------------------------------
        ");
            }
        }

        public void DisplayDetails(int id)
        {
            var post = _postManager.GetById(id);

            if (post is null)
            {
                Console.WriteLine("Post Not Found");
                return;
            }

            var comments = _commentManager.GetByPostId(id);
            Console.WriteLine($@"
            -------------------------------------
            Title: {post.Title}
            Content: {post.Content}
            Post ID: #{post.Id}
            Created At: {post.CreatedAt}
            -------------------------------------
        ");
            int numberOfComments = comments.Count();

            Console.WriteLine((numberOfComments == 0 ? "        No Comments" : $"        Comments ({numberOfComments})\n"));

            foreach (var comment in comments)
            {
                Console.WriteLine($@"
                Comment: {comment.Text}
                Comment ID: #{comment.Id}
                Created At: {comment.CreatedAt}
                -----------------------------
        ");
            }
        }
    }
}