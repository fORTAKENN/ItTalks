using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItTalks.Web.Controllers
{
    public class ForumController : BaseController
    {
        public IActionResult Home()
        {
            return this.View();
        }

        public IActionResult SubmitPost()
        {
            return this.View();
        }


    }
}
