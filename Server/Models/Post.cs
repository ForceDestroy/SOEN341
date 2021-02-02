using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Post
    {
        // Post Details
        public User user { get; set; }
        public string postId { get; set; }
        public string image { get; set; }
        public List<Comment> comments { get; set; }

        // Metrics
        public int numLikes { get; set; }
        public int numHearts { get; set; }

        public Post()
        {
            this.user = null;
            this.postId = null;
            this.image = null;
            this.comments = null;

            this.numLikes = 0;
            this.numHearts = 0;

            // Increase the post count
            this.user.postCount++;
        }

        public Post(User user, string image, List<Comment> comments, int numLikes, int numHearts)
        {
            this.user = user;
            this.postId = $"{this.user.userId}-{this.user.postCount}";
            this.image = image;
            this.comments = comments;

            this.numLikes = numLikes;
            this.numHearts = numHearts;

            // Increase the post count
            this.user.postCount++;
        }

    }
}
