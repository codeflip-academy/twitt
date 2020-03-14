using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwittAPI.Models;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using System.IO;
using SixLabors.ImageSharp.Formats;
using System.Net;
using System.Security.Policy;
using TwittAPI.Presentation;

namespace TwittAPI.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {

        private IConfiguration _config;

        private readonly TwittContext _context;

        public ProfilesController(TwittContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet("{id}")]
        public IActionResult GetProfile(int id)
        {
            var profile = _context.Profile.Find(id);
            var profilePicture = "";

            if (profile == null)
            {
                return NotFound("Profile doesn't exist. :(");
            }
            else if (profile.Picture != null)
            {
                profilePicture = Convert.ToBase64String(profile.Picture);
            }

            var profilePresentation = new ProfilePresentation()
            {
                Id = profile.Id,
                FullName = profile.FullName,
                UserName = profile.UserName,
                Picture = profilePicture,
                Description = profile.Description,
                Status = profile.Status
            };

            return Ok(profilePresentation);
        }

        [HttpPost]
        public IActionResult CreateProfile([FromBody] ProfileModels profile)
        {
            var p = new Profile();
            var usernameExists = _context.Profile.Where(x => x.UserName == profile.UserName).FirstOrDefault() != null;

            if (profile.FullName == null)
            {
                return BadRequest("No Full Name given");
            }
            else if (usernameExists)
            {
                return BadRequest("Username already exists.");
            }
            else if (profile.Password == null)
            {
                return BadRequest("No password given");
            }
            else if (profile != null)
            {
                profile.Status = ProfileState.Active;
            }

            p.FullName = profile.FullName;
            p.UserName = profile.UserName;
            p.Password = profile.Password;
            p.Status = profile.Status;

            _context.Profile.Add(p);

            _context.SaveChanges();

            profile.Id = p.Id;

            if (profile.Picture != null)
            {
                var image = new ImageHandler(connectionString: _config.GetConnectionString("TwittDatabase"));

                image.StoreImageProfile(profile);
            }

            return Ok($"{profile.UserName} was created.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeactivateProfile(int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var profile = _context.Profile.Find(id);
                    profile.Status = false;
                    _context.Profile.Update(profile);
                    _context.SaveChanges();
                    transaction.Commit();
                    return Ok($"{profile.UserName} was deactivated.");
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return BadRequest(e.Message);
                }
            }
        }

    }
}