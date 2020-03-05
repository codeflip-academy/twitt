using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwittAPI.Models;

namespace TwittAPI.Controllers
{
    public class PostFeed
    {
        public PostFeed(List<Post> posts, int page, int pages)
        {
            Posts = new List<Post>(posts);
            Page = page;
            Pages = pages;
        }
        public List<Post> Posts { get; set; }
        public int Page { get; set; }
        public int Pages { get; set; }
    }
}
