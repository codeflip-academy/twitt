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

namespace TwittAPI.Controllers
{
    [Route("api/[controller]")]
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
            if (profile == null)
            {
                return NotFound();
            }
            else if(profile.Picture != null)
            {
                var image = new ImageConverter(connectionString: _config.GetConnectionString("TwittDatabase"));

                image.GetImageFromProfile(profile);
            }
            return Ok(profile);
        }
        
        [HttpPost]
        public IActionResult CreateProfile([FromBody] ProfilePost profile)
        {
            var p = new Profile();
            
            if (profile.FullName == null)
            {
                return BadRequest("No Full Name given");
            }
            
            else if (profile.UserName == null)
            {
                var usernameExists = _context.Profile.Where(x => x.UserName == profile.UserName).FirstOrDefault() != null;
                
                
                if (usernameExists)
                {
                    return BadRequest("Username already exists.");
                }
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
                var image = new ImageConverter(connectionString: _config.GetConnectionString("TwittDatabase"));

                image.StoreImageProfile(profile);
            }

           

            return Ok(profile);
        }

        [HttpDelete("{id}")]
        public IActionResult DeactivateProfile(int id)
        {
            using(var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var profile = _context.Profile.Find(id);
                    profile.Status = false;
                    _context.Profile.Update(profile);
                    _context.SaveChanges();
                    transaction.Commit();
                    return Ok(profile);
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