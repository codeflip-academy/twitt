using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwittAPI.Models
{
    public class ReactionModels
    {
        public string LikeOrDislike { get; set; }
        public int ProfileID { get; set; }
        public int MessageID { get; set; }
    }
}
