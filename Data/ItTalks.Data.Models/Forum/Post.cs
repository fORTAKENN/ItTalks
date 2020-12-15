using System;
using System.Collections.Generic;
using System.Text;

namespace ItTalks.Data.Models.Forum
{
   public class Post
    {
        public Post()
        {
            this.PostId = Guid.NewGuid().ToString();
            this.UserPosts = new HashSet<UserPosts>();
        }
        public string PostId { get; set; }

        public string Email { get; set; }

        public string Messega { get; set; }
        
        public string Name { get; set; }

        public DateTime UpploadData { get; set; }

        public virtual ICollection<UserPosts> UserPosts { get; set; }


    }
}
