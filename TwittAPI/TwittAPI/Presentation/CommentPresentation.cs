using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwittAPI.Models;

namespace TwittAPI.Presentation
{
    public class CommentPresentation
    {
        public CommentPresentation(string message, Profile profile)
        {
            Message = message;
            Profile = new PostProfilePresentation(profile.FullName, profile.UserName, profile.Picture);
        }
        public string Message { get; set; }
        public PostProfilePresentation Profile { get; set; }

    }
}
