using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    interface IUserServices
    {
        User AddUserItems(User user);
        Dictionary<long, User> GetUserItems();
    }
}
