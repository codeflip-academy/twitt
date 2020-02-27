using System;
using System.Collections.Generic;

namespace TwittAPI.Models
{
    public partial class Profile
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public byte[] Picture { get; set; }
        public string Info { get; set; }
        public bool Status { get; set; }
        public bool SignStatus { get; set; }
    }
}
