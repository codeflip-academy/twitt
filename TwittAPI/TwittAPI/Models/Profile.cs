using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TwittAPI.Models
{
    public partial class Profile
    {
        public Profile()
        {
            Comment = new HashSet<Comment>();
            Post = new HashSet<Post>();
            Reaction = new HashSet<Reaction>();
        }
        [JsonIgnore]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public byte[] Picture { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public bool Status { get; set; }

        [JsonIgnore]
        public virtual ICollection<Comment> Comment { get; set; }
        [JsonIgnore]
        public virtual ICollection<Post> Post { get; set; }
        [JsonIgnore]
        public virtual ICollection<Reaction> Reaction { get; set; }
    }
}
