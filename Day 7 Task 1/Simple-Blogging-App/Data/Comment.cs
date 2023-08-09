using System;

namespace Simple_Blogging_App.Data
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public string? Text { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Post? Post { get; set; }
    }
}
