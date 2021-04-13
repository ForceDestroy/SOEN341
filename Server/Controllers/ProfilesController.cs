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
    public class ProfilesController : ControllerBase
    {
        private readonly DatabaseServices _databaseServices;


        public ProfilesController(IDatabaseSettings settings)
        {
            _databaseServices = new DatabaseServices(settings);

        }

        [HttpGet]
        [Route("SearchUsers")]
        public ActionResult<List<User>> SearchUsers(string username)
        {
            var userItems = _databaseServices.Search(username);

            if(userItems == null || userItems.Count == 0)
            {
                return NotFound();
            }

            return userItems;
        }

        //[HttpGet]
        //[Route("GetByUsername")]
        //public ActionResult<User> GetByUsername(string username)
        //{
        //    var userItems = _databaseServices.Get(username);

        //    if (userItems == null)
        //    {
        //        return NotFound();
        //    }

        //    return userItems;
        //}

        [HttpGet]
        [Route("GetByUserId")]
        public ActionResult<User> GetByUserId(int id)
        {
            var userItems = _databaseServices.Get(id);

            if (userItems == null)
            {
                return NotFound();
            }

            return userItems;
        }

    }
}
