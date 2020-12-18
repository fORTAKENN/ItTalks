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
            var post = this.db.Posts.FirstOrDefault(f => f.PostId == postId);

            if (post == null)
            {
                throw new InvalidOperationException("No post found with the given post Id!");
            }

            this.db.Posts.Remove(post);
            this.db.SaveChanges();
        }

        public void EditPost(EditPostModel input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input", "The given input is null!");
            }

            var post = this.db.Posts.FirstOrDefault(f => f.PostId == input.PostId);

            if (post == null)
            {
                throw new InvalidOperationException("No post found with this id!");
            }

            post.Name = input.Name;
            post.Messega = input.Message;

            this.db.SaveChanges();
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

            return posts.OrderBy(p => p.UpploadData).ToList();
        }

        public ICollection<UserPostsViewModel> GetPersonalPosts(string userId)
        {
            var user = this.db.Users.FirstOrDefault(u => u.Id == userId);
            var personalPosts = new List<UserPostsViewModel>();

            var userPosts = this.db.UserPosts.Where(uf => uf.UserId == userId).ToList();
            var postIds = userPosts.Select(uf => uf.PostId).ToArray();

            foreach (var post in this.db.Posts.Where(f => postIds.Contains(f.PostId)))
            {
                personalPosts.Add(new UserPostsViewModel()
                {
                    Email = post.Email,
                    Message = post.Messega,
                    Name = post.Name,
                    UserName = user.UserName,
                    UpploadData = post.UpploadData,
                    PostId = post.PostId
              
                });
            }

            return personalPosts;
        }

        public EditPostModel GetPost(string postId)
        {
            var postDb = this.db.Posts.FirstOrDefault(f => f.PostId == postId);

            if (postDb == null)
            {
                throw new ArgumentNullException("No post found with this id!");
            }

            return new EditPostModel()
            {
                PostId = postDb.PostId,
                Name = postDb.Name,
                Message = postDb.Messega,
            };
        }
    }
}
