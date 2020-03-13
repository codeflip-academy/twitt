using System;
using System.Collections.Generic;

namespace TwittAPI.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int ProfileId { get; set; }
        public int MessageId { get; set; }

        public virtual Message MessageNavigation { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
