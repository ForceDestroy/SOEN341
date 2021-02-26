using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models;

namespace Server.Services
{
    interface IFollowerServices
    {
        MiniUser AddFollowerItems(MiniUser follower);
        Dictionary<long, MiniUser> GetFollowerItems();
        MiniUser RemoveFollowerItems(MiniUser follower);
    }
}
