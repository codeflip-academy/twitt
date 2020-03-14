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
            Profile = new ProfilePresentation()
            {
                Id = profile.Id,
                FullName = profile.FullName,
                UserName = profile.UserName,
                Picture = ValidatePicture(profile),
                Description = profile.Description,
                Status = profile.Status
            };
        }
        public int ID { get; set; }
        public string Message { get; set; }
        public ProfilePresentation Profile { get; set; }


        public string ValidatePicture(Profile profile)
        {
            var image = "";
            if (profile.Picture != null)
            {
                image = Convert.ToBase64String(profile.Picture);
            }
            return image;
        }
    }
}
