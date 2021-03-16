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
    [Route("user/")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseServices _databaseServices;


        public UsersController(IDatabaseSettings settings)
        {
            _databaseServices = new DatabaseServices(settings);
            
        }

        #region User Creation
        [HttpGet]
        [Route("GetNewUserId")]
        public ActionResult<int> GetNewUserId()
        {
            var highestUser = _databaseServices.GetHighest();

            if (highestUser == null)
            {
                return NotFound();
            }
            return highestUser.userId + 1;    
            
            
        }

        [HttpGet]
        [Route("CheckUsername")]
        public ActionResult<User> CheckUsername(String username)
        {
            var userItems = _databaseServices.Get(username);

            if (userItems != null)
            {
                return BadRequest();
            }
            return userItems;
        }

        [HttpPost]
        [Route("CreateNewUser")]
        public ActionResult<User> CreateNewUser(User user)
        {
            if (CheckUsername(user.username).Equals(user.username)){
                return BadRequest();
            }

            if (_databaseServices.Get(user.userId) != null)
            {
                return BadRequest();
            }

            _databaseServices.Create(user);

            return user;
        }

        #endregion


        #region User Login

        [HttpPost]
        [Route("CheckLogin")]
        public ActionResult<User> CheckLogin(string username, string password)
        {
            var userItems = _databaseServices.Get(username);
            
            if (userItems == null)
            {
                return NotFound();
            }

            if (!userItems.password.Equals(password))
            {
                return BadRequest();
            }

            return userItems;
        }

        #endregion



    }
}