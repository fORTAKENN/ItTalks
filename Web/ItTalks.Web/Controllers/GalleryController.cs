﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItTalks.Web.Controllers
{
    public class GalleryController : BaseController
    {
        public IActionResult Home()
        {
            return this.View();
        }




    }
}
