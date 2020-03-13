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
            ID = post.Id;
            Message = post.Text;
            Picture = post.Picture;
            Profile = new PostProfilePresentation(post.Profile.FullName, post.Profile.UserName, post.Profile.Picture);
            Likes = CountLikes(post);
            Dislikes = CountDisLikes(post);
            NumberOfComments = commentCount;
        }
        public int ID { get; set; }
        public string Message { get; set; }
        public byte[] Picture { get; set; }
        public PostProfilePresentation Profile { get; set; }
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
