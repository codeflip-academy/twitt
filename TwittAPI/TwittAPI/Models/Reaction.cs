using System;
using System.Collections.Generic;

namespace TwittAPI.Models
{
    public partial class Reaction
    {
        public int Id { get; set; }
        public bool LikeOrDislike { get; set; }
        public int Profile { get; set; }
        public int Message { get; set; }

        public virtual Message MessageNavigation { get; set; }
        public virtual Profile ProfileNavigation { get; set; }
    }
}
