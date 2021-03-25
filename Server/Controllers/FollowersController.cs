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
        private readonly DatabaseServices _databaseServices;

        public FollowersController(IDatabaseSettings settings)
        {
            _databaseServices = new DatabaseServices(settings);
        }


        [HttpGet]
        [Route("GetFollowers")]
        public ActionResult<List<MiniUser>> GetFollowers(int id)
        {
            var user = _databaseServices.Get(id);

            if (user == null)
            {
                return NotFound();
            }


            return user.followers;
        }

        [HttpGet]
        [Route("GetFollowings")]
        public ActionResult<List<MiniUser>> GetFollowings(int id)
        {
            var user = _databaseServices.Get(id);

            if (user == null)
            {
                return NotFound();
            }


            return user.following;
        }

        [HttpPost]
        [Route("AddFollower")]
        public ActionResult<User> AddFollower(int followerId, int userId)
        {
            if(followerId == userId)
            {
                return Forbid();
            }

            var user = _databaseServices.Get(userId);

            if (user == null)
            {
                return NotFound();
            }

            User followerUser = _databaseServices.Get(followerId);
            
            if (followerUser == null)
            {
                return BadRequest();
            }

            var follower = new MiniUser(followerUser.username, followerUser.userId, followerUser.profilePicture);
            
            if (user.followers.Contains(follower))
            {
                return Forbid();
            }

            user.followers.Add(follower);
            
            // Now Updating the follower's following list
            var miniUser = new MiniUser(user.username, user.userId, user.profilePicture);

            followerUser.following.Add(miniUser);

            _databaseServices.Update(userId, user);
            _databaseServices.Update(followerId, followerUser);

            return user;
        }

        [HttpDelete]
        [Route("RemoveFollower")]
        public ActionResult<User> RemoveFollower(int followerId, int userId)
        {
            if (followerId == userId)
            {
                return Forbid();
            }

            var user = _databaseServices.Get(userId);

            if (user == null)
            {
                return NotFound();
            }

            User followerUser = _databaseServices.Get(followerId);

            if (followerUser == null)
            {
                return BadRequest();
            }

            if (user.followers.RemoveAll(follower => follower.userId == followerId) == 0)
            {
                return Forbid();
            }

            // Now Updating the follower's following list
            followerUser.following.RemoveAll(user => user.userId == userId);

            _databaseServices.Update(userId, user);
            _databaseServices.Update(followerId, followerUser);


            return user;
        }

        [HttpPost]
        [Route("AddFollowing")]
        public ActionResult<User> AddFollowing(int followingId, int userId)
        {
            if (followingId == userId)
            {
                return Forbid();
            }

            var user = _databaseServices.Get(userId);

            if (user == null)
            {
                return NotFound();
            }

            User followingUser = _databaseServices.Get(followingId);

            if (followingUser == null)
            {
                return BadRequest();
            }

            var following= new MiniUser(followingUser.username, followingUser.userId, followingUser.profilePicture);

            if (user.following.Contains(following))
            {
                return Forbid();
            }

            user.following.Add(following);

            // Now Updating the following's follower list
            var miniUser = new MiniUser(user.username, user.userId, user.profilePicture);

            followingUser.followers.Add(miniUser);

            _databaseServices.Update(userId, user);
            _databaseServices.Update(followingId, followingUser);


            return user;
        }

        [HttpDelete]
        [Route("RemoveFollowing")]
        public ActionResult<User> RemoveFollowing(int followingId, int userId)
        {
            if (followingId == userId)
            {
                return Forbid();
            }

            var user = _databaseServices.Get(userId);

            if (user == null)
            {
                return NotFound();
            }

            User followingUser = _databaseServices.Get(followingId);

            if (followingUser == null)
            {
                return BadRequest();
            }

            if(user.following.RemoveAll(following => following.userId == followingId) == 0)
            {
                return Forbid();
            }

            // Now Updating the following's follower list
            followingUser.followers.RemoveAll(user => user.userId == userId);

            _databaseServices.Update(userId, user);
            _databaseServices.Update(followingId, followingUser);

            return user;
        }
    }
}
