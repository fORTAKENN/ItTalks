using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ItTalks.Services.Data
{
    public interface IPostsService
    {
        public Task<ICollection<PostViewModel>> GetAll(string userId);

        public Task EditPost(EditPostInputModel input);
      

        public Task<string> AddPost(AddPostmModel input);
        

        public Task DeletePost(string postId);

        public ICollection<PersonalPostViewModel> GetPersonalPosts(string userId);

        public Task Like(string postId, string userId);

        public EditPostViewModel GetPost(string postId);

        public Task AddPostToUser(string userId, string forumId);


    }
}
