using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwittAPI.Models;

namespace TwittAPI.Presentation
{
    public class CommentFeed
    {
        public CommentFeed(List<CommentPresentation> comments, int page, int pages)
        {
            Comments = new List<CommentPresentation>(comments);
            Page = page;
            Pages = pages;
        }
        public List<CommentPresentation> Comments { get; set; }
        public int Page { get; set; }
        public int Pages { get; set; }
    }
}