using Xunit;
using Simple_Blogging_App.Data;
using Simple_Blogging_App.Manager;

namespace Simple_Blogging_App.Tests
{
    public class CommentManagerTests
    {
        [Fact]
        public void CreateComment_WithValidData_CreatesComment()
        {
            // Arrange
            var commentManager = new CommentManager();
            var newComment = new Comment { PostId = 1, Text = "Test Comment" };

            // Act
            commentManager.CreateComment(newComment);

            // Assert
            Assert.NotEqual(0, newComment.CommentId);
        }

        [Fact]
        public void GetCommentById_WithNonExistingId_ReturnsNull()
        {
            // Arrange
            var commentManager = new CommentManager();

            // Act
            var result = commentManager.GetCommentById(-1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetCommentById_WithExistingId_ReturnsCorrectComment()
        {
            // Arrange
            var commentManager = new CommentManager();
            var newComment = new Comment { PostId = 1, Text = "Test Comment" };
            commentManager.CreateComment(newComment);

            // Act
            var result = commentManager.GetCommentById(newComment.CommentId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Comment", result.Text);
        }

        [Fact]
        public void UpdateComment_WithValidData_UpdatesComment()
        {
            // Arrange
            var commentManager = new CommentManager();
            var newComment = new Comment { PostId = 1, Text = "Test Comment" };
            commentManager.CreateComment(newComment);

            // Act
            newComment.Text = "Updated Test Comment";
            commentManager.UpdateComment(newComment);

            // Assert
            var updatedComment = commentManager.GetCommentById(newComment.CommentId);
            Assert.NotNull(updatedComment);
            Assert.Equal("Updated Test Comment", updatedComment.Text);
        }

        [Fact]
        public void UpdateComment_WithNonExistingId_DoesNotUpdate()
        {
            // Arrange
            var commentManager = new CommentManager();
            var newComment = new Comment { PostId = 1, Text = "Test Comment" };

            // Act
            newComment.Text = "Updated Test Comment";
            commentManager.UpdateComment(newComment);

            // Assert
            var updatedComment = commentManager.GetCommentById(newComment.CommentId);
            Assert.Null(updatedComment);
        }

        [Fact]
        public void DeleteComment_WithValidId_DeletesComment()
        {
            // Arrange
            var commentManager = new CommentManager();
            var newComment = new Comment { PostId = 1, Text = "Test Comment" };
            commentManager.CreateComment(newComment);

            // Act
            commentManager.DeleteComment(newComment.CommentId);

            // Assert
            var deletedComment = commentManager.GetCommentById(newComment.CommentId);
            Assert.Null(deletedComment);
        }

        [Fact]
        public void DeleteComment_WithNonExistingId_DoesNotDelete()
        {
            // Arrange
            var commentManager = new CommentManager();

            // Act
            commentManager.DeleteComment(-1);

            // Assert
            var deletedComment = commentManager.GetCommentById(-1);
            Assert.Null(deletedComment);
        }
    

        [Fact]
        public void CreateComment_WithEmptyText_DoesNotCreateComment()
        {
            // Arrange
            var commentManager = new CommentManager();
            var newComment = new Comment { PostId = 1, Text = "" };

            // Act
            commentManager.CreateComment(newComment);

            // Assert
            var createdComment = commentManager.GetCommentById(newComment.CommentId);
            Assert.Null(createdComment);
        }

        [Fact]
        public void UpdateComment_WithEmptyText_DoesNotUpdateComment()
        {
            // Arrange
            var commentManager = new CommentManager();
            var newComment = new Comment { PostId = 1, Text = "Test Comment" };
            commentManager.CreateComment(newComment);

            // Act
            newComment.Text = "";
            commentManager.UpdateComment(newComment);

            // Assert
            var updatedComment = commentManager.GetCommentById(newComment.CommentId);
            Assert.NotNull(updatedComment);
            Assert.Equal("Test Comment", updatedComment.Text);
        }

        [Fact]
        public void DeleteComment_WithInvalidId_DoesNotDeleteComment()
        {
            // Arrange
            var commentManager = new CommentManager();

            // Act
            commentManager.DeleteComment(-1);

            // Assert
            var deletedComment = commentManager.GetCommentById(-1);
            Assert.Null(deletedComment);
        }

        [Fact]
        public void UpdateComment_WithNonexistentId_DoesNotUpdateComment()
        {
            // Arrange
            var commentManager = new CommentManager();
            var newComment = new Comment { PostId = 1, Text = "Test Comment" };
            commentManager.CreateComment(newComment);

            // Act
            newComment.CommentId = -1; // Nonexistent ID
            newComment.Text = "Updated Comment";
            commentManager.UpdateComment(newComment);

            // Assert
            var updatedComment = commentManager.GetCommentById(newComment.CommentId);
            Assert.Null(updatedComment);
        }

        [Fact]
        public void DeleteComment_WithExistingId_DeletesCommentAndUpdatesPostComments()
        {
            // Arrange
            var postManager = new PostManager();
            var commentManager = new CommentManager();
            var newPost = new Post { Title = "Test Post", Content = "Content" };
            postManager.CreatePost(newPost);
            var newComment = new Comment { PostId = newPost.PostId, Text = "Test Comment" };
            commentManager.CreateComment(newComment);

            // Act
            commentManager.DeleteComment(newComment.CommentId);

            // Assert
            var deletedComment = commentManager.GetCommentById(newComment.CommentId);
            var updatedPost = postManager.GetPostById(newPost.PostId);
            Assert.Null(deletedComment);
            Assert.Empty(updatedPost.Comments);
        }
    }
}