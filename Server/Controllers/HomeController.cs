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
    [Route("home/")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly DatabaseServices _databaseServices;
        private int maxNumberOfPosts;

        public HomeController(IDatabaseSettings settings)
        {
            _databaseServices = new DatabaseServices(settings);
            maxNumberOfPosts = 20;
        }

        [HttpGet]
        [Route("GetHomePosts")]
        public ActionResult<List<Post>> GetHomePosts(int userId)
        {
            var user = _databaseServices.Get(userId);

            if(user == null)
            {
                return NotFound();
            }

            List<Post> fPosts = new List<Post>();

            foreach(MiniUser f in user.following)
            {
                var fUser = _databaseServices.Get((int) f.userId);

                if(fUser == null)
                {
                    return BadRequest();
                }

                fPosts.AddRange(fUser.posts);

            }

            fPosts = fPosts.OrderByDescending(x => x.date).ToList();
            fPosts = fPosts.Take(maxNumberOfPosts).ToList();


            return fPosts;
        }

        [HttpGet]
        [Route("Test")]

        public ActionResult<Boolean> Test(int id, int idd)
        {
            var user = _databaseServices.Get(id);

            var user2 = _databaseServices.Get(idd);

            var miniUser = new MiniUser(user.username, user.userId, user.profilePicture);

            var miniUser2 = new MiniUser(user2.username, user2.userId, user2.profilePicture);

            return miniUser.Equals(miniUser2);
        }



    }
}
