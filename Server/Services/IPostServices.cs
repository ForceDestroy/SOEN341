using System;
using Server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    interface IPostServices
    {
        Post AddPostItems(Post post);
        Post DeletePostItems(Post post);
        Dictionary<string, Post> GetPostItems();
        

        Comment AddCommentItems(Comment comment);
        List<Comment> GetCommentItems();

        MiniUser AddLikeItems(MiniUser like);
        Dictionary<long, MiniUser> GetLikeItems();

        MiniUser AddHeartItems(MiniUser heart);
        Dictionary<long, MiniUser> GetHeartItems();

        


        
    }
}
