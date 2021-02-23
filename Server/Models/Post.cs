using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Post
    {
        // Post Details
        public int userId { get; set; }
        public string postId { get; set; }
        public string image { get; set; }
        public string caption { get; set; }
        public List<Comment> comments { get; set; }
        public DateTime date { get; set; }

        // Metrics
        public List<int> likes { get; set; }
        public List<int> hearts { get; set; }

        // Reactions List
        public List<User> likesList { get; set; }
        public List<User> heartsList { get; set; }

        public Post()
        {
            this.userId = 0;
            this.postId = null;
            this.image = null;
            this.caption = null;
            this.comments = null;
            this.date = new DateTime();

            this.likes = null;
            this.hearts = null;
        }

        public Post(int userId, string image, string caption, List<Comment> comments, DateTime date, List<int> likes, List<int> hearts)
        {
            this.userId = userId;
            this.postId = null;
            this.image = image;
            this.caption = caption;
            this.comments = comments;
            this.date = date;

            this.likes = likes;
            this.hearts = hearts;

        }

    }
}
