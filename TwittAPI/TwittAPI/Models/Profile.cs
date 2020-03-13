using System;
using System.Collections.Generic;

namespace TwittAPI.Models
{
    public partial class Profile
    {
        public Profile()
        {
            Comment = new HashSet<Comment>();
            Message = new HashSet<Message>();
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
        public virtual ICollection<Message> Message { get; set; }
        public virtual ICollection<Reaction> Reaction { get; set; }
    }
}
