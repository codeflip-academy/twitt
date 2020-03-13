using System;
using System.Collections.Generic;

namespace TwittAPI.Models
{
    public partial class Message
    {
        public Message()
        {
            Comment = new HashSet<Comment>();
            Reaction = new HashSet<Reaction>();
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public byte[] Picture { get; set; }
        public int ProfileId { get; set; }

        public virtual Profile Profile { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Reaction> Reaction { get; set; }
    }
}
