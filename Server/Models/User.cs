using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Server.Models
{
    public class User
    {
        // Database id
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        private string _id;

        // Account Info
        public string username { get; set; }
        public string password { get; set; }
        public int userId { get; set; }

        // About the user
        public string name { get; set; }
        public string about { get; set; }
        public string profilePicture { get; set; }
        
        // Social info
        public List<MiniUser> followers { get; set; }
        public List<MiniUser> following { get; set; }
        public List<Post> posts { get; set; }

        // Post Tracking
        public int postCount { get; set; }

        public User()
        {
            //this._id = null;

            this.username = null;
            this.password = null;
            this.userId = -1;

            this.name = null;
            this.about = null;
            this.profilePicture = null;

            this.followers = null;
            this.following = null;
            this.posts = null;

            this.postCount = 0;
        }

        public User(string _id, string username, string password, int userId, string name, string about, string profilePicture, List<MiniUser> followers, List<MiniUser> following, List<Post> posts, int postCount)
        {
            //this._id = _id;
            this.username = username;
            this.password = password;
            this.userId = userId;

            this.name = name;
            this.about = about;
            this.profilePicture = profilePicture;

            this.followers = followers;
            this.following = following;
            this.posts = posts;

            this.postCount = postCount;
        }
        
    }
}
