using System;
using System.Collections.Generic;
using System.Text;

namespace ItTalks.Data.Models.Forum
{
    public class UserPosts
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
