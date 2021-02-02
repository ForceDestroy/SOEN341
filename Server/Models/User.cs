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
        public string about { get; set; }
        public string profilePic { get; set; }
        
        // Social info
        public List<User> followers { get; set; }
        public List<User> followings { get; set; }
        public List<Post> posts { get; set; }

        // Post Tracking
        public int postCount { get; set; }

        public User()
        {
            this.username = null;
            this.password = null;

            this.name = null;
            this.about = null;
            this.profilePic = null;

            this.followers = null;
            this.followings = null;
            this.posts = null;

            this.postCount = 0;
        }

        public User(string username, string password, long userId, string name, string about, string profilePic, List<User> followers, List<User> followings, List<Post> posts, int postCount)
        {
            this.username = username;
            this.password = password;

            this.name = name;
            this.about = about;
            this.profilePic = profilePic;

            this.followers = followers;
            this.followings = followings;
            this.posts = posts;

            this.postCount = postCount;
        }
        
    }
}
