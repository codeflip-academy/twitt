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
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public byte[] Picture { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Post> Post { get; set; }
        public virtual ICollection<Reaction> Reaction { get; set; }
    }
}
