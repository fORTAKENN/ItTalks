using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

 namespace ItTalks.Web.ViewModels.Gallery
{
   public class PhotoInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public IFormFile Photo { get; set; }


    }
}
