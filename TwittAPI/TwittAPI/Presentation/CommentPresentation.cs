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
            Profile = new PostProfilePresentation(profile.FullName, profile.UserName, profile.Picture);
        }
        public int ID { get; set; }
        public string Message { get; set; }
        public PostProfilePresentation Profile { get; set; }

    }
}
