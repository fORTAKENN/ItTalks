
namespace ItTalks.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ItTalks.Data;
    using ItTalks.Data.Models;
    using ItTalks.Data.Models.Forum;
    using ItTalks.Web.ViewModels.Forum;
    using Microsoft.EntityFrameworkCore;

    using Xunit;


   public class PostsServiceTests
    {

        [Fact]
        public void AddPostShouldAddPostandReturnsId()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("PostTest");
            var db = new ApplicationDbContext(options.Options);

            var name = "Name";
            var email = "Email@a.b";
            var message = "messege";

            var InputAdd = new ForumInputModel()
            {
                Message = message,
                Email = email,
                Name = name
            };

            var countBeforeAdding = db.Posts.Count();
            var service = new PostsService(db);

            var postId = service.AddPost(InputAdd);
            var postDb = db.Posts.FirstOrDefault(f => f.PostId == postId);

            Assert.Equal(countBeforeAdding + 1, db.Posts.Count());
            Assert.NotNull(postDb);
        }
        [Fact]
        public void AddPostShouldThrowArgumentNullExceptionIfNotGivenInput()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("PostsTest");
            var db = new ApplicationDbContext(options.Options);

            var service = new PostsService(db);

           Assert.Throws<ArgumentNullException>( () =>  service.AddPost(null));
        }

        [Fact]
        public void AddPostToUserShouldAddTheForumIdToTheUserWithId()
        {

            var name = "Name";
            var email = "Email@a.b";
            var message = "messege";

            var forum = new Post()
            {
                Messega = message,
                Email = email,
                Name = name,
                
            };

            var user = new ApplicationUser()
            {
                Email = "george.arbk@test.com",
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestAddPost");
            var db = new ApplicationDbContext(options.Options);

             db.Posts.Add(forum);
             db.Users.Add(user);
             db.SaveChanges();

            var service = new PostsService(db);

            int countBeforeAdd = db.UserPosts.Count();

            service.AddPostToUser(user.Id, forum.PostId);

            var uf = db.UserPosts.FirstOrDefault(uf => uf.PostId == forum.PostId && uf.UserId == user.Id);

            Assert.NotNull(uf);
            Assert.Equal(countBeforeAdd + 1, db.UserPosts.Count());
        }

        [Fact]
        public void AddPostToUserShouldThrowArgumentNullExceptionPostIdIsNull()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Posts");
            var db = new ApplicationDbContext(options.Options);

            var service = new PostsService(db);

            Assert.Throws<ArgumentNullException>(() => service.AddPostToUser("real_user_id", null));
        }

        [Fact]
        public void AddPostToUserShouldThrowArgumentNullExceptionPostIdIsInvalid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("AddPosts");
            var db = new ApplicationDbContext(options.Options);

            var service = new PostsService(db);

             Assert.Throws<ArgumentNullException>(() => service.AddPostToUser("real_user_id", "fake_post_id"));
        }

        [Fact]
        public void DeletePostShouldRemoveThePostIfGivenValidPostId()
        {

            var name = "Name";
            var email = "Email@a.b";
            var message = "messege";

            var forum = new Post()
            {
                Messega = message,
                Email = email,
                Name = name,

            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("PostsTestAdd");
            var db = new ApplicationDbContext(options.Options);

             db.Posts.Add(forum);
             db.SaveChanges();

            int countBeforeDelete = db.Posts.Count();
            var service = new PostsService(db);

            service.DeletePost(forum.PostId);

            var forumDb = db.Posts.FirstOrDefault(f => f.PostId == forum.PostId);

            Assert.Null(forumDb);
            Assert.Equal(countBeforeDelete - 1, db.Posts.Count());
        }

        [Fact]
        public void DeletePostShouldThrowInvalidOperationExceptionIfInvalidPostId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestPost");
            var db = new ApplicationDbContext(options.Options);

            var service = new PostsService(db);

            Assert.Throws<InvalidOperationException>(() =>  service.DeletePost("fake_post_id"));
        }

        [Fact]
        public void EditPostShouldEditTheGivenPostWithTheGivenEditPostModel()
        {
            var name = "Name";
            var email = "Email@a.b";
            var message = "messege";

            var forum = new Post()
            {
                Messega = message,
                Email = email,
                Name = name,

            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("EditPost");
            var db = new ApplicationDbContext(options.Options);

            db.Posts.Add(forum);
             db.SaveChanges();

            var service = new PostsService(db);

            string editedName = "EditedName";
            string editedMessage = "EditedMessage";

            var editInput = new EditPostModel()
            {
                PostId = forum.PostId,
                Name = editedName,
               Message = editedMessage,
            };

            service.EditPost(editInput);

            var forumDb = db.Posts.FirstOrDefault(f => f.PostId == forum.PostId);

            Assert.NotNull(forumDb);
            Assert.Equal(editedName, forumDb.Name);
            Assert.Equal(editedMessage, forumDb.Messega);
        }

        [Fact]
        public void EditPostShouldThrowArgumentNullExceptionIfNotGivenInput()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("EditTestPost");
            var db = new ApplicationDbContext(options.Options);

            var service = new PostsService(db);

             Assert.Throws<ArgumentNullException>(() => service.EditPost(null));
        }

        [Fact]
        public void EditPostShouldThrowInvalidOperationExceptionIfInvalidPostId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("PostTestEdit");
            var db = new ApplicationDbContext(options.Options);

            var service = new PostsService(db);

            string editedName = "EditedName";
            string editedMessage = "EditedMessage";

            var editInput = new EditPostModel()
            {
                PostId = "fake_post_id",
                Name = editedName,
                Message = editedMessage,
            };

             Assert.Throws<InvalidOperationException>(() => service.EditPost(editInput));
        }

        [Fact]
        public void GetAllShouldReturnCollectionOfPostViewModelOfAllPostsInDatase()
        {
            var user = new ApplicationUser()
            {
                Email = "george.arbk@test.com",
            };

            var name = "Name";
            var email = "Email@a.b";
            var message = "messege";

            var forum = new Post()
            {
                Messega = message,
                Email = email,
                Name = name,

            };

            var forum2 = new Post()
            {
                Messega = message,
                Email = email,
                Name = name,

            };

            var forums = new List<Post>();
            forums.Add(forum);
            forums.Add(forum2);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseInMemoryDatabase("PostTestGetAll");
            var db = new ApplicationDbContext(options.Options);

             db.Users.Add(user);
            db.Posts.AddRange(forums);
             db.SaveChanges();

            int expectedCount = forums.Count();

            var service = new PostsService(db);

            var result = service.GetAll();

            Assert.NotNull(result);
            Assert.Equal(expectedCount, result.Count());
            Assert.IsType<PostViewModel>(result.First());
        }

        [Fact]
        public void GetPersonalPostsShouldReturnCollectionOfAllPostPostsMadeByTheUser()
        {
            var user = new ApplicationUser()
            {
                Email = "george.arbk@test.com",
            };
            var name = "Name";
            var email = "Email@a.b";
            var message = "messege";

            var forum = new Post()
            {
                Messega = message,
                Email = email,
                Name = name,

            };

            var forum2 = new Post()
            {
                Messega = message,
                Email = email,
                Name = name,

            };


            var forums = new List<Post>();
            forums.Add(forum);
            forums.Add(forum2);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseInMemoryDatabase("TestGetPosts");
            var db = new ApplicationDbContext(options.Options);

             db.Users.Add(user);
            db.Posts.AddRange(forums);
             db.SaveChanges();

            int expectedCount = forums.Count();

            var service = new PostsService(db);

           

           service.AddPostToUser(user.Id, forum.PostId);
             service.AddPostToUser(user.Id, forum2.PostId);

           

            var result = service.GetPersonalPosts(user.Id);

            Assert.NotNull(result);
            Assert.Equal(expectedCount, result.Count());
            Assert.IsType<UserPostsViewModel>(result.First());
        }

        [Fact]
        public void GetPostShouldReturnThePostWithTheGivenId()
        {
            var user = new ApplicationUser()
            {
                Email = "george.arbk@test.com",
            };
            var name = "Name";
            var email = "Email@a.b";
            var message = "messege";
            var newForum = new Post()
            {
               Name = name,
                Email = email,
                Messega = message
            };
            var userForum = new UserPosts()
            {
                PostId = newForum.PostId,
                Post = newForum,
                User = user,
                UserId = user.Id
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestGetPostNew");
            var db = new ApplicationDbContext(options.Options);

             db.Posts.Add(newForum);
             db.Users.Add(user);
             db.UserPosts.Add(userForum);
             db.SaveChanges();

            var service = new PostsService(db);

            var result = service.GetPost(newForum.PostId);

            Assert.NotNull(result);
            Assert.IsType<EditPostModel>(result);
            Assert.Equal(name, result.Name);
            Assert.Equal(message, result.Message);
           
        }

        [Fact]
        public void GetPostShouldThrowArgumentNullExceptionIfIdIsInvalid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
.UseInMemoryDatabase("GetPost");
            var db = new ApplicationDbContext(options.Options);

            var service = new PostsService(db);

            Assert.Throws<ArgumentNullException>(() => service.GetPost("fake_post_id"));
        }

    }
}
