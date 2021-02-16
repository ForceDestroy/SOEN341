using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    public class UserServices : IUserServices
    {
        private readonly Dictionary<long, User> _userItems;

        public UserServices()
        {
            _userItems = new Dictionary<long, User>();
        }

        public User AddUserItems(User user)
        {
            _userItems.Add(user.userId, user);

            return user;
        }

        public Dictionary<long, User> GetUserItems()
        {
            return _userItems;
        }
    }
}
