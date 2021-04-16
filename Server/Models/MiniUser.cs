using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    public class MiniUser: IEquatable<MiniUser>
    {
        public string username { get; set; }
        public long userId { get; set; }
        public string profilePicture { get; set; }

        public MiniUser()
        {
            this.username = null;
            this.userId = 0;
            this.profilePicture = null;
        }

        public MiniUser(string username, long userId, string profilePicture)
        {
            this.username = username;
            this.userId = userId;
            this.profilePicture = profilePicture;
        }

        public Boolean Equals(MiniUser miniUser)
        {
            return username.Equals(miniUser.username) && userId == miniUser.userId;
        }
    }
}
