using ItTalks.Data;
using ItTalks.Data.Models.Forum;
using ItTalks.Web.ViewModels.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItTalks.Services.Data
{
    public class PostsService : IPostsService
    {
        private readonly ApplicationDbContext db;
        public PostsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public string AddPost(ForumInputModel input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input", "The given input is null!");
            }

            var post = new Post()
            {
                Name = input.Name,
                Email = input.Email,
                Messega = input.Message,
                UpploadData = DateTime.Now

            };

             this.db.Posts.Add(post);

             this.db.SaveChanges();

            return post.PostId;
        }

        public void AddPostToUser(string userId, string postId)
        {
            if (string.IsNullOrEmpty(postId))
            {
                throw new ArgumentNullException("postId", "The given forum Id is null or emtpy string!");
            }

            var post =  this.db.Posts.FirstOrDefault(f => f.PostId == postId);

            if (post == null)
            {
                throw new ArgumentNullException("forumId", "The given forum Id not valid!");
            }

            this.db.UserPosts.Add(new UserPosts()
            {
                UserId = userId,
                PostId = post.PostId,
            });

             this.db.SaveChanges();
        }

        public void DeletePost(string postId)
        {
            throw new NotImplementedException();
        }

        public void EditPost(EditPostInputModel input)
        {
            throw new NotImplementedException();
        }

        public ICollection<PostViewModel> GetAll()
        {
            var posts = new List<PostViewModel>();
            foreach (var post in this.db.Posts.ToList())
            {
                posts.Add(new PostViewModel()
                {
                  Email = post.Email,
                  Name = post.Name,
                  Message = post.Messega,
                  UpploadData = post.UpploadData

                });
            }

            return posts;
        }

        public ICollection<UserPostsViewModel> GetPersonalPosts(string userId)
        {
            throw new NotImplementedException();
        }

        public EditPostModel GetPost(string postId)
        {
            throw new NotImplementedException();
        }
    }
}
