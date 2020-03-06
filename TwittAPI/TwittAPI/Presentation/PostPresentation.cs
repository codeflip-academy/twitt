using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwittAPI.Models;

namespace TwittAPI.Presentation
{
    public class PostPresentation
    {
        public PostPresentation(Post post, int? commentCount, Profile profile)
        {
            PostPicture = post.Picture;
            Message = post.Message;
            CommentCount = commentCount;
            Profile = profile;
        }
        public byte[] PostPicture { get; set; }
        public string Message { get; set; }
        public int? CommentCount { get; set; }
        public Profile Profile { get; set; }
    }
}
