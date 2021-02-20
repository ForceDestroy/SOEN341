using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    [Route("profile/")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _services = new UserServices();

        [HttpPost]
        [Route("AddUserItems")]
        public ActionResult<User> AddUserItems(User user)
        {
            var userItems = _services.AddUserItems(user);

            if (user == null)
            {
                return NotFound();
            }

            return userItems;
        }

        [HttpGet]
        [Route("GetUserItems")]
        public ActionResult<Dictionary<long, User>> GetUserItems()
        {
            var userItems = _services.GetUserItems();

            if (userItems.Count == 0)
            {
                return NotFound();
            }

            return userItems;                                                    

        }

        [HttpPut]
        [Route("ChangeUserItems")]
        public ActionResult<Dictionary<long, User>> ChangeUserItems(User user)
        {
            var userItems = _services.GetUserItems();

            if (userItems.Count == 0 || user == null || !userItems.ContainsKey(user.userId)) {
                return NotFound();
            }

            userItems.Remove(user.userId);
            userItems.Add(user.userId, user);

            return userItems;
        }

        [HttpDelete]
        [Route("DeleteUserItems")]
        public ActionResult<Dictionary<long, User>> DeleteUserItems(User user)
        {
            var userItems = _services.GetUserItems();

            if (userItems.Count == 0 || user == null || !userItems.ContainsKey(user.userId))
            {
                return NotFound();
            }

            userItems.Remove(user.userId);

            return userItems;
        }

        //Review later

        //[HttpPost]
        //[Route("LoginUserItems")]
        //public ActionResult<Dictionary<long, User>> LoginUserItems(User user)
        //{
        //    var userItems = _services.GetUserItems();

        //    if (userItems.Count == 0 || user == null || !userItems.ContainsKey(user.userId))
        //    {
        //        return NotFound();
        //    }

        //    userItems.Remove(user.userId);
        //    userItems.Add(user.userId, user);

        //    return userItems;
        //}


    }
}
