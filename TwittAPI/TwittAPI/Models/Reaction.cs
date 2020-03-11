using System;
using System.Collections.Generic;

namespace TwittAPI.Models
{
    public partial class Reaction
    {
        public int Id { get; set; }
        public bool State { get; set; }
        public int Profile { get; set; }
        public int Post { get; set; }
        public virtual Twitt PostNavigation { get; set; }
        public virtual Profile ProfileNavigation { get; set; }
    }
}