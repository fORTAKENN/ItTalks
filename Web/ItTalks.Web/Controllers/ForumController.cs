using ItTalks.Services.Data;
using ItTalks.Web.ViewModels.Forum;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ItTalks.Web.Controllers
{
    public class ForumController : BaseController
    {
        private readonly IPostsService postService;

        public ForumController(IPostsService postService)
        {
            this.postService = postService;
        }

        public IActionResult Home()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }
            var viewModel = this.postService.GetAll();

            return this.View(viewModel);
        }

        public IActionResult SubmitPost()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }
            return this.View();
        }

        [HttpPost]
         public IActionResult SubmitPost(ForumInputModel model)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var postId = this.postService.AddPost(model);
            this.postService.AddPostToUser(userId,postId);

            return this.Redirect("/Forum/Home");
        }
        public IActionResult MyPosts()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var viewModel = this.postService.GetPersonalPosts(userId);

            return this.View(viewModel);
        }


        public IActionResult Delete(string postId)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }
            this.postService.DeletePost(postId);
            return this.Redirect("/Forum/MyPosts");


        }
        [HttpPost]
        public IActionResult EditPost(string name, string message, string postId)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }
            var input = new EditPostModel()
            {
                Message = message,
                Name = name,
                PostId = postId
            };
            this.postService.EditPost(input);
            return this.Redirect("/Forum/MyPosts");


        }

        public IActionResult EditPost(string postId)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }
            var ViewModel = this.postService.GetPost(postId);
            return this.View(ViewModel);
        }
    }
}
