using System;
using System.Collections.Generic;

namespace Simple_Blogging_App.Data
{
    public class Post
    {
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<Comment>? Comments { get; set; }
    }
}
