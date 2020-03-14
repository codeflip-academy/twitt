using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwittAPI.Models;

namespace TwittAPI.Presentation
{
    public class PostPresentation
    {
        public PostPresentation(Message post, int? commentCount)
        {
            PostID = post.Id;
            UserName = post.Profile.UserName;
            Message = post.Text;
            Picture = post.Picture;
            Likes = CountLikes(post);
            Dislikes = CountDisLikes(post);
            NumberOfComments = commentCount;
        }
        public int PostID { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public byte[] Picture { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int? NumberOfComments { get; set; }

        public int CountLikes(Message message)
        {
            int likes = 0;
            foreach (var reaction in message.Reaction)
            {
                if(reaction.LikeOrDislike == true)
                {
                    likes++;
                }
            }
            return likes;
        }

        public int CountDisLikes(Message message)
        {
            int disLikes = 0;
            foreach (var reaction in message.Reaction)
            {
                if (reaction.LikeOrDislike == false)
                {
                    disLikes++;
                }
            }
            return disLikes;
        }
    }
}
