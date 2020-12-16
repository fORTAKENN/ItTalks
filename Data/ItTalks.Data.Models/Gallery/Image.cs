using System;
using System.Collections.Generic;
using System.Text;

namespace ItTalks.Data.Models.Gallery
{
   public class Image
    {
        public Image()
        {
            this.ImageId = Guid.NewGuid().ToString();
         
        }
        public string ImageId { get; set; }

        public string ImageUrl { get; set; }

        public string Name { get; set; }

        public DateTime UpploadData { get; set; }

        public string OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }

    }
}
