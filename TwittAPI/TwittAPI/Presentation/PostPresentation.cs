using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwittAPI.Models;

namespace TwittAPI.Presentation
{
    public class PostPresentation
    {
        public PostPresentation(Post post, int? commentCount)
        {
            Post = post;
            CommentCount = commentCount;
        }
        public Post Post { get; set; }
        public int? CommentCount { get; set; }
    }
}
