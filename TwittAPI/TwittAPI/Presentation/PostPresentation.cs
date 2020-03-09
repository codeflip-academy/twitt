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
        public PostPresentation(Post post, int? commentCount, int likes, int dislikes)
        {
            ID = post.Id;
            Message = post.Message;
            Picture = post.Picture;
            Profile = new PostProfilePresentation(post.Profile.FullName, post.Profile.UserName, post.Profile.Picture);
            Likes = likes;
            Dislikes = dislikes;
            NumberOfComments = commentCount;
        }
        public int ID { get; set; }
        public string Message { get; set; }
        public byte[] Picture { get; set; }
        public PostProfilePresentation Profile { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int? NumberOfComments { get; set; }
    }
}
