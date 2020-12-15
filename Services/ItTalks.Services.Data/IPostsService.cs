using ItTalks.Web.ViewModels.Forum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ItTalks.Services.Data
{
    public interface IPostsService
    {
        public  ICollection<PostViewModel> GetAll();

        public void EditPost(EditPostModel input);
        public  string AddPost(ForumInputModel input);
        public void DeletePost(string postId);
        public ICollection<UserPostsViewModel> GetPersonalPosts(string userId);

        public EditPostModel GetPost(string postId);

        public void AddPostToUser(string userId, string forumId);
    }
}
