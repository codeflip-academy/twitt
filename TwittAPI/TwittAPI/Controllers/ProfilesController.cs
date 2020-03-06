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
           
            var InsertedData = _context.Profile.Add(profile);
            if(InsertedData == null)
            {
                return NotFound("Profile not valid");
            }
            return Ok(InsertedData);
        }
    }
}
