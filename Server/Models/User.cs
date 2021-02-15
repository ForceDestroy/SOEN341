using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class User
    {
        // Account Info
        public string username { get; set; }
        public string password { get; set; }
        public long userId { get; set; }

        // About the user
        public string name { get; set; }
        public string profilePicture { get; set; }
        
        // Social info
        public List<MiniUser> followers { get; set; }
        public List<Post> posts { get; set; }

        // Post Tracking
        public int postCount { get; set; }

        public User()
        {
            this.username = null;
            this.password = null;

            this.name = null;
            this.profilePicture = null;

            this.followers = null;
            this.posts = null;

            this.postCount = 0;
        }

        public User(string username, string password, long userId, string name, string profilePicture, List<MiniUser> followers, List<Post> posts, int postCount)
        {
            this.username = username;
            this.password = password;

            this.name = name;
            this.profilePicture = profilePicture;

            this.followers = followers;
            this.posts = posts;

            this.postCount = postCount;
        }
        
    }
}
