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
        private readonly IPostServices _services = new PostServices();

        [HttpPost]
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


        [HttpGet]
        [Route("GetPostItems")]
        public ActionResult<Dictionary<string, Post>> GetPostItems()
        {
            var postItems = _services.GetPostItems();

            if (postItems.Count == 0)
            {
                return NotFound();
            }

            return postItems;

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
    }
}
