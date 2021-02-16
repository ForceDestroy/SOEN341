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
    [Route("follower/")]
    [ApiController]
    public class FollowersController : ControllerBase
    {
        private readonly IFollowerServices _services = new FollowerServices();

        [HttpGet]
        [Route("GetFollowerItems")]
        public ActionResult<Dictionary<long, MiniUser>> GetFollowerItems()
        {
            var followerItems = _services.GetFollowerItems();

            if (followerItems.Count == 0)
            {
                return NotFound();
            }

            return followerItems;
        }

        [HttpPost]
        [Route("AddFollowerItems")]
        public ActionResult<MiniUser> AddFollowerItems(MiniUser follower)
        {
            if (follower == null)
            {
                return NotFound();
            }
            var followerItems = _services.AddFollowerItems(follower);

            return followerItems;
        }

        [HttpDelete]
        [Route("DeleteFollowerItems")]
        public ActionResult<Dictionary<long, Post>> DeleteFollowerItems(MiniUser follower)
        {

            var followerItem = _services.GetFollowerItems();

            if (followerItem.Count == 0 || follower == null || !followerItem.ContainsValue(follower))
            {
                return NoContent();
            }

            var followerItems = _services.RemoveFollowerItems(follower);


            return Ok();
        }
    }
}
