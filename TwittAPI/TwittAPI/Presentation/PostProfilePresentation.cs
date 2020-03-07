using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwittAPI.Presentation
{
    public class PostProfilePresentation
    {
        public PostProfilePresentation(string fullName, string userName, byte[] picture)
        {
            FullName = fullName;
            UserName = userName;
            Picture = picture;
        }

        public string FullName { get; set; }
        public string UserName { get; set; }
        public byte[] Picture { get; set; }
    }
}
