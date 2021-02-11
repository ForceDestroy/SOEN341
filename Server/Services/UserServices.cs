using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    public class UserServices : IUserServices
    {
        private readonly Dictionary<string, User> _userItems;

        public UserServices()
        {
            _userItems = new Dictionary<string, User>();
        }

        public User AddUserItems(User user)
        {
            _userItems.Add(user.name, user);

            return user;
        }

        public Dictionary<string, User> GetUserItems()
        {
            return _userItems;
        }
    }
}
