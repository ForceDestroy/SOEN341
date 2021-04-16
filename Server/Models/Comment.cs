using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Comment
    {
        public string username{ get; set; }
        public string postId { get; set; }
        public string content { get; set; }

        public Comment()
        {
            this.username = "";
            this.postId = null;
            this.content = null;
        }

        public Comment(string username, string postId, string content)
        {
            this.username = username;
            this.postId = postId;
            this.content = content;
        }
    }
}
