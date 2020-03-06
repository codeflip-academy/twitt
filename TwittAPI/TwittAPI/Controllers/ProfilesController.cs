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
            if(profile == null)
            {
                return NotFound();
            }
            return Ok(profile);
        }

        [HttpPost]
        public IActionResult CreateProfile([FromBody] Profile profile)
        {
            if (profile.Id <= 0)
            {
                return BadRequest("Id didn't update correctly");
            }
            else if (profile.FullName == null)
            {
                return BadRequest("No Full Name given");
            }
            else if (profile.UserName == null)
            {
                return BadRequest("No User Name Given");
            }
            else if (profile.Password == null)
            {
                return BadRequest("No password given");
            }
            else if (profile.Picture != null)
            {
                //store image in profile table
            }
            else if (profile != null)
            {
                profile.Status = ProfileState.Active;
            }
            
            _context.Profile.Add(profile);
            
            _context.SaveChangesAsync();
            
            return CreatedAtAction("GetProfile", new { id = profile.Id }, profile);
        }
        

    }
}
