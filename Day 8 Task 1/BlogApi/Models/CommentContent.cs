namespace BlogApi.Models;

public class CommentContent
{
    public int PostId { get; set; }
    public string Text { get; set; } = "";
}