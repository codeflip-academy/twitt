using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwittAPI.Models;

namespace TwittAPI.Presentation
{
    public class CommentPresentation
    {
        public CommentPresentation(int id, string message, Profile profile)
        {
            ID = id;
            Message = message;
            UserName = profile.UserName;
        }
        public int ID { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
    }
}
