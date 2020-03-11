using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwittAPI.Models
{
    public class TwittModels
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Picture { get; set; }
        public int ProfileId { get; set; }
    }
}
