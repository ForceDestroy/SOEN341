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
    [Route("profiles/")]
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
        public ActionResult<Dictionary<string, User>> GetUserItems()
        {
            var userItems = _services.GetUserItems();

            if (userItems.Count == 0)
            {
                return NotFound();
            }

            return userItems;                                                    

        }

    }
}
