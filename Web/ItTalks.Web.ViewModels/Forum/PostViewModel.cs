using System;
using System.Collections.Generic;
using System.Text;

namespace ItTalks.Web.ViewModels.Forum
{
   public class PostViewModel
    {
        public string Message { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime UpploadData { get; set; }
    }
}
