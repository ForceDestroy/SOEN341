using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Services;
using Server.Models;


namespace Server.Controllers
{
    [Route("post/")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly DatabaseServices _databaseServices;


        public PostsController(IDatabaseSettings settings)
        {
            _databaseServices = new DatabaseServices(settings);

        }
        
        [HttpGet]
        [Route("GetPost")]
        public ActionResult<Post> GetPost(string postId)
        {
            var user = _databaseServices.Get(Convert.ToInt32(postId.Substring(0, postId.IndexOf("-"))));

            if (user == null)
            {
                return NotFound();
            }

            var postItem = user.posts.Find(post => post.postId.Equals(postId));
            if (postItem == null)
            {
                return NotFound();
            }

            return postItem;

        }

        //[HttpGet]
        //[Route("GetPostsByUsername")]
        //public ActionResult<List<Post>> GetPostsByUsername(string username)
        //{
        //    var user = _databaseServices.Get(username);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    var postItems = user.posts;
            
        //    if (postItems == null)
        //    {
        //        return NotFound();
        //    }

        //    return postItems;

        //}

        [HttpGet]
        [Route("GetPostsByUserId")]
        public ActionResult<List<Post>> GetPostsByUserId(int id)
        {
            var user = _databaseServices.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            var postItems = user.posts;

            if (postItems == null)
            {
                return NotFound();
            }

            return postItems;

        }
        [HttpGet]
        [Route("GetNewPostId")]
        public ActionResult<int> GetNewPostId(int id)
        {
            var user = _databaseServices.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            var postCount = user.postCount;
            if (postCount < 0)
            {
                return BadRequest();
            }

            return postCount + 1;

        }
        [HttpPost]
        [Route("AddNewPost")]
        public ActionResult<Post> AddNewPost(Post post)
        {
            var user = _databaseServices.Get(post.userId);

            if(user == null)
            {
                return NotFound();
            }

            if (user.posts.Contains(post))
            {
                return BadRequest();
            }

            user.posts.Add(post);

            user.postCount++;
            _databaseServices.Update(post.userId, user);

            return post;
        }

        [HttpDelete]
        [Route("RemovePost")]
        public ActionResult<User> RemovePost(string postId)
        {
            var user = _databaseServices.Get(Convert.ToInt32(postId.Substring(0, postId.IndexOf("-"))));

            if (user == null)
            {
                return NotFound();
            }

            if (user.posts.RemoveAll(post => post.postId.Equals(postId)) == 0)
            {
                return BadRequest();
            }

            _databaseServices.Update(user.userId, user);
            return user;
        }


        [HttpPost]
        [Route("AddNewComment")]
        public ActionResult<Comment> AddNewComment(Comment comment)
        {
            var user = _databaseServices.Get(Convert.ToInt32(comment.postId.Substring(0, comment.postId.IndexOf("-"))));

            if (user == null)
            {
                return NotFound();
            }

            var post = user.posts.Find(p => p.postId.Equals(comment.postId));

            if (post == null)
            {
                return BadRequest();
            }

            post.comments.Add(comment);

            
            _databaseServices.Update(user.userId, user);

            return comment;
        }

        [HttpPost]
        [Route("AddLikeReaction")]
        public ActionResult<List<int>> AddLikeReaction(int userId, string postId)
        {
            var postUser = _databaseServices.Get(Convert.ToInt32(postId.Substring(0, postId.IndexOf("-"))));

            if (postUser == null)
            {
                return NotFound();
            }

            var post = postUser.posts.Find(p => p.postId.Equals(postId));

            if (post == null)
            {
                return NotFound();
            }

            if (post.likes.Contains(userId))
            {
                return BadRequest();
            }

            post.likes.Add(userId);

            _databaseServices.Update(post.userId, postUser);

            return post.likes;

        }

        [HttpPost]
        [Route("AddHeartReaction")]
        public ActionResult<List<int>> AddHeartReaction(int userId, string postId)
        {
            var postUser = _databaseServices.Get(Convert.ToInt32(postId.Substring(0, postId.IndexOf("-"))));

            if (postUser == null)
            {
                return NotFound();
            }

            var post = postUser.posts.Find(p => p.postId.Equals(postId));

            if (post == null)
            {
                return NotFound();
            }

            if (post.hearts.Contains(userId))
            {
                return BadRequest();
            }

            post.hearts.Add(userId);

            _databaseServices.Update(post.userId, postUser);

            return post.hearts;

        }


        /*[HttpPost]
        [Route("AddPostItems")]
        public ActionResult<Post> AddPostItems(Post post)
        {
            if (post == null)
            {
                return NotFound();
            }

            var postItems = _services.AddPostItems(post);

            return postItems;
        }

        [HttpPost]
        [Route("AddCommentItems")]
        public ActionResult<Comment> AddCommentItems(Comment comment)
        {
            if (comment == null)
            {
                return NotFound();
            }
            var commentItems = _services.AddCommentItems(comment);

            return commentItems;
        }

        [HttpPost]
        [Route("AddLikeItems")]
        public ActionResult<MiniUser> AddLikeItems(MiniUser like)
        {
            if (like == null)
            {
                return NotFound();
            }
            var likeItems = _services.AddLikeItems(like);

            return likeItems;
        }

        [HttpPost]
        [Route("AddHeartItems")]
        public ActionResult<MiniUser> AddHeartItems(MiniUser heart)
        {
            if (heart == null)
            {
                return NotFound();
            }
            var heartItems = _services.AddHeartItems(heart);

            return heartItems;
        }


        

        [HttpDelete]
        [Route("DeletePostItems")]
        public ActionResult<Dictionary<string, Post>> DeletePostItems(Post post)
        {

            var postItem = _services.GetPostItems();

            if (postItem.Count == 0 || post == null || !postItem.ContainsValue(post))
            {
                return NotFound();
            }

            var postItems = _services.DeletePostItems(post);

            
            return Ok();
        
    }
        */
    }
}
