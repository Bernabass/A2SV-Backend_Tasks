using Xunit;
using Simple_Blogging_App.Data;
using Simple_Blogging_App.Manager;

namespace Simple_Blogging_App.Tests
{
    public class PostManagerTests
    {
        [Fact]
        public void CreatePost_WithValidData_CreatesPost()
        {
            // Arrange
            var postManager = new PostManager();
            var newPost = new Post { Title = "Test Post", Content = "Content" };

            // Act
            postManager.CreatePost(newPost);

            // Assert
            Assert.NotEqual(0, newPost.PostId);
        }

        [Fact]
        public void GetPostById_WithNonExistingId_ReturnsNull()
        {
            // Arrange
            var postManager = new PostManager();

            // Act
            var result = postManager.GetPostById(-1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetPostById_WithExistingId_ReturnsCorrectPost()
        {
            // Arrange
            var postManager = new PostManager();
            var newPost = new Post { Title = "Test Post", Content = "Content" };
            postManager.CreatePost(newPost);

            // Act
            var result = postManager.GetPostById(newPost.PostId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Post", result.Title);
        }

        [Fact]
        public void UpdatePost_WithValidData_UpdatesPost()
        {
            // Arrange
            var postManager = new PostManager();
            var newPost = new Post { Title = "Test Post", Content = "Content" };
            postManager.CreatePost(newPost);

            // Act
            newPost.Title = "Updated Test Post";
            newPost.Content = "Updated Content";
            postManager.UpdatePost(newPost);

            // Assert
            var updatedPost = postManager.GetPostById(newPost.PostId);
            Assert.NotNull(updatedPost);
            Assert.Equal("Updated Test Post", updatedPost.Title);
            Assert.Equal("Updated Content", updatedPost.Content);
        }

        [Fact]
        public void UpdatePost_WithNonExistingId_DoesNotUpdate()
        {
            // Arrange
            var postManager = new PostManager();
            var newPost = new Post { Title = "Test Post", Content = "Content" };

            // Act
            newPost.Title = "Updated Test Post";
            newPost.Content = "Updated Content";
            postManager.UpdatePost(newPost);

            // Assert
            var updatedPost = postManager.GetPostById(newPost.PostId);
            Assert.Null(updatedPost);
        }

        [Fact]
        public void DeletePost_WithValidId_DeletesPost()
        {
            // Arrange
            var postManager = new PostManager();
            var newPost = new Post { Title = "Test Post", Content = "Content" };
            postManager.CreatePost(newPost);

            // Act
            postManager.DeletePost(newPost.PostId);

            // Assert
            var deletedPost = postManager.GetPostById(newPost.PostId);
            Assert.Null(deletedPost);
        }

        [Fact]
        public void DeletePost_WithNonExistingId_DoesNotDelete()
        {
            // Arrange
            var postManager = new PostManager();

            // Act
            postManager.DeletePost(-1);

            // Assert
            var deletedPost = postManager.GetPostById(-1);
            Assert.Null(deletedPost);
        }
    

        [Fact]
        public void CreatePost_WithEmptyTitle_DoesNotCreatePost()
        {
            // Arrange
            var postManager = new PostManager();
            var newPost = new Post { Title = "", Content = "Content" };

            // Act
            postManager.CreatePost(newPost);

            // Assert
            var createdPost = postManager.GetPostById(newPost.PostId);
            Assert.Null(createdPost);
        }

        [Fact]
        public void UpdatePost_WithEmptyTitle_DoesNotUpdatePost()
        {
            // Arrange
            var postManager = new PostManager();
            var newPost = new Post { Title = "Test Post", Content = "Content" };
            postManager.CreatePost(newPost);

            // Act
            newPost.Title = "";
            postManager.UpdatePost(newPost);

            // Assert
            var updatedPost = postManager.GetPostById(newPost.PostId);
            Assert.NotNull(updatedPost);
            Assert.Equal("Test Post", updatedPost.Title);
        }

        [Fact]
        public void DeletePost_WithInvalidId_DoesNotDeletePost()
        {
            // Arrange
            var postManager = new PostManager();

            // Act
            postManager.DeletePost(-1);

            // Assert
            var deletedPost = postManager.GetPostById(-1);
            Assert.Null(deletedPost);
        }

        [Fact]
        public void DeletePost_WithExistingId_DeletesPostAndAssociatedComments()
        {
            // Arrange
            var postManager = new PostManager();
            var commentManager = new CommentManager();
            var newPost = new Post { Title = "Test Post", Content = "Content" };
            postManager.CreatePost(newPost);
            var newComment = new Comment { PostId = newPost.PostId, Text = "Test Comment" };
            commentManager.CreateComment(newComment);

            // Act
            postManager.DeletePost(newPost.PostId);

            // Assert
            var deletedPost = postManager.GetPostById(newPost.PostId);
            var deletedComment = commentManager.GetCommentById(newComment.CommentId);
            Assert.Null(deletedPost);
            Assert.Null(deletedComment);
        }

    }
}
