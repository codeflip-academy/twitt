using System;
using System.Collections.Generic;

namespace TwittAPI.Models
{
    public partial class CommentsCount
    {
        public int MessageId { get; set; }
        public int? CommentCount { get; set; }
    }
}
