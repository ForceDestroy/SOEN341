using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    public class FollowerServices : IFollowerServices
    {
        private readonly Dictionary<long, MiniUser> _followerItems;

        public FollowerServices()
        {
            _followerItems = new Dictionary<long, MiniUser>();
        }
        public MiniUser AddFollowerItems(MiniUser follower)
        {
            _followerItems.Add(follower.userId, follower);
            return follower;
        }

        public Dictionary<long, MiniUser> GetFollowerItems()
        {
            return _followerItems;
        }

        public MiniUser RemoveFollowerItems(MiniUser follower)
        {
            _followerItems.Remove(follower.userId);
            return follower;
        }
    }
}
