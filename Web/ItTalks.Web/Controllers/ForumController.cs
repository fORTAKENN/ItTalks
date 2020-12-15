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
            var viewModel = this.postService.GetAll();
            return this.View(viewModel);
        }

        public IActionResult SubmitPost()
        {
            return this.View();
        }

        [HttpPost]
         public IActionResult SubmitPost(ForumInputModel model)
        {
           var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var postId = this.postService.AddPost(model);
            this.postService.AddPostToUser(userId,postId);

            return this.Redirect("/Forum/Home");
        }

    }
}
