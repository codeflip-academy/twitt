using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwittAPI.Models
{
    public class ReactionModels
    {
        public int Id { get; set; }
        public string LikeOrDislike { get; set; }
        public int Profile { get; set; }
        public int Post { get; set; }
        public virtual Message PostNavigation { get; set; }
        public virtual Profile ProfileNavigation { get; set; }
    }
}
