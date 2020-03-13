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
        public IActionResult Reaction(ReactionModels reaction)
        {
            var react = new Reaction();
            if (reaction.ProfileID != 0 && reaction.MessageID != 0)
            {
                var userReactions = _context.Reaction
                    .Where(r => r.Profile == reaction.ProfileID && r.Message == reaction.MessageID)
                    .Count();

                react.Message = reaction.MessageID;
                react.Profile = reaction.ProfileID;

                if (userReactions < 1)
                {
                    _context.Reaction.Add(react);
                }
                else
                {
                    return BadRequest("Cannont give same reaction twice.");
                }


                _context.SaveChanges();
                return Ok(reaction);
            }
            return BadRequest("Data is missing from the request.");
        }
    }
}