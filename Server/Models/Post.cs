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
        public List<MiniUser> likes { get; set; }
        public List<MiniUser> hearts { get; set; }

        public Post()
        {
            this.user = null;
            this.postId = null;
            this.image = null;
            this.comments = null;

            this.likes = null;
            this.hearts = null;
        }

        public Post(User user, string image, List<Comment> comments, List<MiniUser> likes, List<MiniUser> hearts)
        {
            this.user = user;
            this.postId = $"{this.user.userId}-{this.user.postCount}";
            this.image = image;
            this.comments = comments;

            this.likes = likes;
            this.hearts = hearts;

            // Increase the post count
            this.user.postCount++;
        }

    }
}
