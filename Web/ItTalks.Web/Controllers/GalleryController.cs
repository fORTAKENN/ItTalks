using ItTalks.Services.Data;
using ItTalks.Web.ViewModels.Gallery;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ItTalks.Web.Controllers
{
    public class GalleryController : BaseController
    {
        private readonly IGalleryService galleryService;

        public GalleryController(IGalleryService galleryService)
        {
            this.galleryService = galleryService;
        }

        public IActionResult Home()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }
            var ViewModel = this.galleryService.GetAll();
            return this.View(ViewModel);
        }

        public IActionResult UpploadPhotos()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }
            return this.View();
        }

        [HttpPost]
        public IActionResult UpploadPhotos(PhotoInputModel model)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            this.galleryService.AddImage(model, userId);


            return this.Redirect("/Gallery/Home");
        }

        public IActionResult MyPhotos()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var ViewModel = this.galleryService.GetPersonalPhotos(userId);
            return this.View(ViewModel);
        }


    }
}
