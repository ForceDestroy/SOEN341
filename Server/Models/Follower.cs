using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class Follower
    {
        public string username { get; set; }
        public long userId { get; set; }
        public string profilePicture { get; set; }

        public Follower()
        {
            this.username = null;
            this.userId = 0;
            this.profilePicture = null;
        }

        public Follower(string username, long userId, string profilePicture)
        {
            this.username = username;
            this.userId = userId;
            this.profilePicture = profilePicture;
        }
    }
}
