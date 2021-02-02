using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Comment
    {
        public User user { get; set; }
        public string content { get; set; }

        public Comment()
        {
            this.user = null;
            this.content = null;
        }

        public Comment(User user, string content)
        {
            this.user = user;
            this.content = content;
        }
    }
}
