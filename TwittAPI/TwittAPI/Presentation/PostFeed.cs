using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwittAPI.Models;
using TwittAPI.Presentation;

namespace TwittAPI.Controllers
{
    public class PostFeed
    {
        public PostFeed(List<PostPresentation> posts, int page, int pages)
        {
            Posts = new List<PostPresentation>(posts);
            Page = page;
            Pages = pages;
        }
        public List<PostPresentation> Posts { get; set; }
        public int Page { get; set; }
        public int Pages { get; set; }
    }
}
