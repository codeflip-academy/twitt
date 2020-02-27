using System;
using System.Collections.Generic;

namespace TwittAPI.Models
{
    public partial class Post
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public byte[] Picture { get; set; }
        public string Author { get; set; }
    }
}
