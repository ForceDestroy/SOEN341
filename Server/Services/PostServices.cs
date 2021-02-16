using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models;

namespace Server.Services
{
    public class PostServices : IPostServices
    {
        private readonly Dictionary<string, Post> _postItems;
        private readonly List<Comment> _commentItems; 
        private readonly Dictionary<long, MiniUser> _likeItems;
        private readonly Dictionary<long, MiniUser> _heartItems;

        public PostServices()
        {
            _postItems = new Dictionary<string, Post>();
            _commentItems = new List<Comment>();
            _likeItems = new Dictionary<long, MiniUser>();
            _heartItems = new Dictionary<long, MiniUser>();
    }

        public Comment AddCommentItems(Comment comment)
        {
            _commentItems.Add(comment);

            return comment;
        }

        public MiniUser AddHeartItems(MiniUser heart)
        {
            _heartItems.Add(heart.userId, heart);

            return heart;
        }

        public MiniUser AddLikeItems(MiniUser like)
        {
            _likeItems.Add(like.userId, like);

            return like;
        }

        public Post AddPostItems(Post post)
        {
            _postItems.Add(post.postId, post);

            return post;
        }

        public Post DeletePostItems(Post post)
        {
            _postItems.Remove(post.postId);

            return post;
        }

        public List<Comment> GetCommentItems()
        {
            return _commentItems;
        }

        public Dictionary<long, MiniUser> GetHeartItems()
        {
            return _heartItems;
        }

        public Dictionary<long, MiniUser> GetLikeItems()
        {
            return _likeItems;
        }

        public Dictionary<string, Post> GetPostItems()
        {
            return _postItems;
        }

       
    }
}
