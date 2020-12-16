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
        public IActionResult Home()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/Identity/Account/Login");
            }
            return this.View();
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


            return this.Redirect("/Gallery/Home");
        }



    }
}
