using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TwittAPI.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int ProfileId { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
