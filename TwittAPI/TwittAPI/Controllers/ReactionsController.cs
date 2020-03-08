using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwittAPI.Models;
using Microsoft.Extensions.Configuration;


namespace TwittAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactionsController : ControllerBase
    {

        private IConfiguration _config;

        private readonly TwittContext _context;

        public ReactionsController(TwittContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost]
        public IActionResult Reaction(Reaction reaction)
        {
            if(reaction.Profile != 0 && reaction.Post != 0)
            {
                var userReactions = _context.Reaction
                    .Where(r => r.Profile == reaction.Profile && r.Post == reaction.Post)
                    .Count();

                if(userReactions < 1)
                {
                    _context.Reaction.Add(reaction);
                }
                else
                {
                    return BadRequest("User already reacted to this post.");
                }

                _context.SaveChanges();
                return Ok(reaction);
            }
            return BadRequest("Data is missing from the request.");
        }
    }
}
