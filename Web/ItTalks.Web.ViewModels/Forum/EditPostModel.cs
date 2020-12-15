using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ItTalks.Web.ViewModels.Forum
{
   public class EditPostModel
    {
        [Required]
        public string Message { get; set; }
        [Required]
        public string Name { get; set; }

        public string PostId { get; set; }

    }
}
