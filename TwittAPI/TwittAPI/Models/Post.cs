using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TwittAPI.Models
{
    public partial class Post
    {
        public Post()
        {
            Comment = new HashSet<Comment>();
            Reaction = new HashSet<Reaction>();
        }
        public int Id { get; set; }
        public string Message { get; set; }
        public byte[] Picture { get; set; }
        public int ProfileId { get; set; }

        [JsonIgnore]
        public virtual Profile Profile { get; set; }
        [JsonIgnore]
        public virtual ICollection<Comment> Comment { get; set; }
        [JsonIgnore]
        public virtual ICollection<Reaction> Reaction { get; set; }
    }
}
