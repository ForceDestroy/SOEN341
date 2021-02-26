using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Comment
    {
        public int userId { get; set; }
        public string postId { get; set; }
        public string content { get; set; }

        public Comment()
        {
            this.userId = 0;
            this.postId = null;
            this.content = null;
        }

        public Comment(int userId, string postId, string content)
        {
            this.userId = userId;
            this.postId = postId;
            this.content = content;
        }
    }
}
